(function () {
    const config = window.populationCrudConfig;
    if (!config) {
        return;
    }

    let skipCount = 0;
    const maxResultCount = 10;
    let editingId = null;

    const tableBody = document.getElementById('crudTableBody');
    const pagerInfo = document.getElementById('crudPagerInfo');
    const searchInput = document.getElementById('crudSearchInput');
    const form = document.getElementById('crudForm');
    const modal = document.getElementById('crudModal');
    const title = document.getElementById('crudTitle');
    const saveButton = document.getElementById('crudSaveBtn');
    const createButton = document.getElementById('crudCreateBtn');
    const prevButton = document.getElementById('crudPrevBtn');
    const nextButton = document.getElementById('crudNextBtn');
    const exportCsvButton = document.getElementById('crudExportCsvBtn');
    const exportExcelButton = document.getElementById('crudExportExcelBtn');
    const headerRow = document.getElementById('crudHeaderRow');

    function getInputValue(field) {
        const el = document.getElementById(`f_${field.name}`);
        if (!el) return null;
        if (field.readOnly && field.excludeFromPayload) return undefined;
        if (field.type === 'checkbox') return !!el.checked;
        if (field.type === 'number') return el.value ? Number(el.value) : 0;
        if (field.type === 'date') return el.value || null;
        if (field.type === 'select') {
            if (!el.value) return null;
            return field.valueType === 'number' ? Number(el.value) : el.value;
        }
        return el.value || null;
    }

    function setInputValue(field, value) {
        const el = document.getElementById(`f_${field.name}`);
        if (!el) return;
        if (field.type === 'checkbox') {
            el.checked = !!value;
            return;
        }
        const selectedValue = value ?? '';
        if (field.type === 'select' && selectedValue && !Array.from(el.options).some(x => x.value === selectedValue)) {
            const option = document.createElement('option');
            option.value = selectedValue;
            option.text = selectedValue;
            el.appendChild(option);
        }
        el.value = selectedValue;
    }

    async function loadLookupOptions(field) {
        if (!field.lookupUrl) {
            return field.options || [];
        }

        const result = await abp.ajax({
            url: field.lookupUrl,
            type: 'GET',
            data: { skipCount: 0, maxResultCount: 1000 }
        });

        return (result.items || []).map(x => ({ value: x.id, text: x.name || x.wardName || x.houseNumber || x.id }));
    }

    async function buildForm() {
        const htmlParts = [];
        for (const field of config.fields) {
            if (field.type === 'checkbox') {
                htmlParts.push(`<div class="mb-2 form-check">
                    <input type="checkbox" class="form-check-input" id="f_${field.name}">
                    <label class="form-check-label" for="f_${field.name}">${field.label}</label>
                </div>`);
                continue;
            }

            if (field.type === 'select') {
                const options = await loadLookupOptions(field);
                const optionsHtml = [`<option value="">Select</option>`]
                    .concat(options.map(o => `<option value="${o.value}">${o.text}</option>`))
                    .join('');

                htmlParts.push(`<div class="mb-2">
                    <label class="form-label" for="f_${field.name}">${field.label}</label>
                    <select class="form-select" id="f_${field.name}" ${field.readOnly ? 'disabled' : ''}>${optionsHtml}</select>
                </div>`);
                continue;
            }

            htmlParts.push(`<div class="mb-2">
                <label class="form-label" for="f_${field.name}">${field.label}</label>
                <input type="${field.type || 'text'}" class="form-control" id="f_${field.name}" ${field.readOnly ? 'readonly' : ''}>
            </div>`);
        }

        form.innerHTML = htmlParts.join('');

        for (const field of config.fields) {
            if (!field.onChange) continue;
            const el = document.getElementById(`f_${field.name}`);
            if (!el) continue;
            el.addEventListener('change', async function () {
                const payload = {};
                config.fields.forEach(f => {
                    const value = getInputValue(f);
                    if (value !== undefined) {
                        payload[f.name] = value;
                    }
                });
                await field.onChange({ field, element: el, payload, setInputValue, getInputValue, config });
            });
        }
    }

    function buildHeader() {
        if (!headerRow) return;
        headerRow.innerHTML = config.columns
            .map(col => `<th>${col.label || col.name}</th>`)
            .join('') + '<th></th>';
    }

    function buildRow(item) {
        const cols = config.columns.map(col => `<td>${(item[col.name] ?? '').toString()}</td>`).join('');
        return `<tr>
            ${cols}
            <td class="text-end">
                <button class="btn btn-sm btn-secondary me-1" data-edit="${item.id}">Edit</button>
                <button class="btn btn-sm btn-danger" data-delete="${item.id}">Delete</button>
            </td>
        </tr>`;
    }

    async function loadData() {
        const result = await abp.ajax({
            url: config.apiBaseUrl,
            type: 'GET',
            data: {
                skipCount: skipCount,
                maxResultCount: maxResultCount,
                filter: searchInput.value || null
            }
        });

        tableBody.innerHTML = (result.items || []).map(buildRow).join('');
        pagerInfo.innerText = `Showing ${skipCount + 1}-${Math.min(skipCount + maxResultCount, result.totalCount || 0)} of ${result.totalCount || 0}`;
        prevButton.disabled = skipCount <= 0;
        nextButton.disabled = skipCount + maxResultCount >= (result.totalCount || 0);
    }

    async function openForEdit(id) {
        const item = await abp.ajax({ url: `${config.apiBaseUrl}/${id}`, type: 'GET' });
        editingId = id;
        title.innerText = `${config.entityName} - Edit`;
        config.fields.forEach(f => setInputValue(f, item[f.name]));
        modal.style.display = 'block';
    }

    function openForCreate() {
        editingId = null;
        title.innerText = `${config.entityName} - Create`;
        config.fields.forEach(f => setInputValue(f, f.defaultValue ?? null));
        modal.style.display = 'block';
    }

    async function remove(id) {
        if (!confirm('Are you sure?')) return;
        await abp.ajax({ url: `${config.apiBaseUrl}/${id}`, type: 'DELETE' });
        await loadData();
    }

    async function save() {
        saveButton.disabled = true;
        const payload = {};
        config.fields.forEach(f => {
            const value = getInputValue(f);
            if (value !== undefined) {
                payload[f.name] = value;
            }
        });

        if (editingId) {
            await abp.ajax({ url: `${config.apiBaseUrl}/${editingId}`, type: 'PUT', data: JSON.stringify(payload) });
        } else {
            await abp.ajax({ url: config.apiBaseUrl, type: 'POST', data: JSON.stringify(payload) });
        }

        modal.style.display = 'none';
        saveButton.disabled = false;
        await loadData();
    }

    function closeModal() {
        modal.style.display = 'none';
    }

    async function exportFile(type) {
        const endpoint = type === 'csv' ? 'export-csv' : 'export-excel';
        const response = await fetch(`${config.apiBaseUrl}/${endpoint}?filter=${encodeURIComponent(searchInput.value || '')}&skipCount=0&maxResultCount=1000`, {
            credentials: 'include'
        });
        const blob = await response.blob();
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = type === 'csv' ? `${config.entityName.toLowerCase()}.csv` : `${config.entityName.toLowerCase()}.xls`;
        a.click();
        window.URL.revokeObjectURL(url);
    }

    tableBody.addEventListener('click', async function (e) {
        const editId = e.target.getAttribute('data-edit');
        if (editId) {
            await openForEdit(editId);
            return;
        }
        const deleteId = e.target.getAttribute('data-delete');
        if (deleteId) {
            await remove(deleteId);
        }
    });

    document.getElementById('crudModalCloseBtn').addEventListener('click', closeModal);
    createButton.addEventListener('click', openForCreate);
    saveButton.addEventListener('click', save);
    document.getElementById('crudSearchBtn').addEventListener('click', async function () {
        skipCount = 0;
        await loadData();
    });
    prevButton.addEventListener('click', async function () {
        skipCount = Math.max(0, skipCount - maxResultCount);
        await loadData();
    });
    nextButton.addEventListener('click', async function () {
        skipCount += maxResultCount;
        await loadData();
    });

    if (exportCsvButton) exportCsvButton.addEventListener('click', () => exportFile('csv'));
    if (exportExcelButton) exportExcelButton.addEventListener('click', () => exportFile('excel'));

    async function initialize() {
        await buildForm();
        buildHeader();
        await loadData();
    }

    initialize();
})();
