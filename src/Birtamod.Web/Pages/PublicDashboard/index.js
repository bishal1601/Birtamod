let publicChart = null;

async function loadPublicDashboard() {
    const ward = document.getElementById('wardFilter').value || null;
    const result = await abp.ajax({
        url: '/api/app/dashboard/public-dashboard',
        type: 'GET',
        data: { filter: ward, skipCount: 0, maxResultCount: 10 }
    });

    const summary = result.summary;
    const cards = [
        { name: 'Total Households', value: summary.totalHouseholds },
        { name: 'Total Population', value: summary.totalPopulation },
        { name: 'Total Male', value: summary.totalMale },
        { name: 'Total Female', value: summary.totalFemale },
        { name: 'Total Other Gender', value: summary.totalOtherGender }
    ];
    document.getElementById('publicSummaryCards').innerHTML = cards.map(c => `
        <div class="col-md-4 col-lg-2 mb-2">
            <div class="card"><div class="card-body">
                <div class="text-muted small">${c.name}</div>
                <div class="h4 mb-0">${c.value}</div>
            </div></div>
        </div>
    `).join('');

    const gender = result.populationByGender || [];
    if (publicChart) {
        publicChart.destroy();
    }
    publicChart = new Chart(document.getElementById('publicGenderChart'), {
        type: 'bar',
        data: {
            labels: gender.map(x => x.name),
            datasets: [{ data: gender.map(x => x.count), label: 'Population by Gender' }]
        }
    });
}

document.getElementById('loadDashboard').addEventListener('click', loadPublicDashboard);
loadPublicDashboard();
