drawSpecialtiesChart();

function drawSpecialtiesChart() {
	$.get({
		url: '/Dashboard/GetDoctorsPerSpecialty',
		success: function (figure) {

			const ctx = document.getElementById('doughnutChart');

			const data = {
				labels: figure.map(f => f.label),
				datasets: [{
					label: 'My First Dataset',
					data: figure.map(f => f.value),
					backgroundColor: [
						'rgb(255, 99, 132)',
						'rgb(54, 162, 235)',
						'rgb(255, 205, 86)',
						'rgb(255, 205, 80)',
						'#5F91B6',
						'#D3F6FC',
						'#C8B0D2'
					],
					hoverOffset: 7
				}]
			};

			new Chart(ctx, {
				type: 'doughnut',
				data: data,
			});
		}
	});
};