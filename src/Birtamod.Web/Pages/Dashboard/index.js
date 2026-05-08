(async function () {
    const result = await abp.ajax({
        url: '/api/app/dashboard/admin-dashboard',
        type: 'GET'
    });

    const summary = result.summary;
    const cards = [
        { name: 'Total Households', value: summary.totalHouseholds },
        { name: 'Total Population', value: summary.totalPopulation },
        { name: 'Total Male', value: summary.totalMale },
        { name: 'Total Female', value: summary.totalFemale },
        { name: 'Total Other Gender', value: summary.totalOtherGender }
    ];

    document.getElementById('summaryCards').innerHTML = cards.map(c => `
        <div class="col-md-4 col-lg-2 mb-2">
            <div class="card"><div class="card-body">
                <div class="text-muted small">${c.name}</div>
                <div class="h4 mb-0">${c.value}</div>
            </div></div>
        </div>
    `).join('');

    const gender = result.populationByGender || [];
    const ctx = document.getElementById('genderChart');
    new Chart(ctx, {
        type: 'pie',
        data: {
            labels: gender.map(x => x.name),
            datasets: [{ data: gender.map(x => x.count) }]
        }
    });
})();
