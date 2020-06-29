$(document).ready(function () {
    Grafico();
});
function GerarPdf(Table) {
    html2canvas($('' + Table), {
        onrendered: function (canvas) {
            var imgData = canvas.toDataURL('image/png');
            var pdf = new jsPDF('p', 'mm');
            pdf.addImage(imgData, 'PNG', 25, 10);
            pdf.save('Grafico.pdf');
        }
    });
};
function ExibirGrafico(Id, Btn) {
    var visivel = $('#' + Id)
    var element = document.getElementById('' + Btn);

    if (visivel.is(':visible')) {
        visivel.hide(100);
        element.classList.remove('fa-eye-slash');
        element.classList.add('fa-eye');

    }
    else {
        visivel.show(100);
        element.classList.remove('fa-eye');
        element.classList.add('fa-eye-slash');
    }
};
function Grafico() {
    $("#divLoading").show(400);

    $.ajax({
        type: 'POST',
        url: '/Home/Grafico',
        async: false,
        data: {

        },
        success: function (result) {

            if (result != null) {
                var i = 0;
                var dados = [];
                while (result[i] != null) {
                    dados.push(result[i]);
                    i++
                }
                GraficoAtivoRegional(dados);
                GraficoImagem(result[i+1]);
                GraficoSomaAtivos(result[i+2]);
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Ops...',
                    type: 'error',
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    text: 'Deu Algo errado',
                    timer: 5000
                })
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
};
function MontarLabelRegional(dados) {
    var label = [];
    var Texto = '';


    $.each(dados, function () {
        label.push(this.regional.replace('REGIONAL', ''))
    });

    return label;
};
function MontarDataAtivos(dados) {
    var Data = [];

    $.each(dados, function () {
        Data.push(this.quantidade)
    });

    return Data;
};
function GraficoAtivoRegional(dados) {
    var ctx = document.getElementById('ativosregional').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: MontarLabelRegional(dados),
            datasets: [{
                label: ['Ativos'],
                data: MontarDataAtivos(dados),
                backgroundColor: [
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(153, 204, 50, 0.2)',
                    'rgba(255, 127, 0, 0.2)',
                    'rgba(79, 47, 79, 0.2)'
                ],
                borderColor: [
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 99, 132, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(153, 204, 50, 1)',
                    'rgba(255, 127, 0, 1)',
                    'rgba(79, 47, 79, 1)'
                ],
                borderWidth: 2
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
};

function GraficoImagem(dados) {
    var ctx = document.getElementById('imagemAtivos').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['C/Imagem','S/Imagem'],
            datasets: [{
                data: [dados.quantidade, dados.quantidadeTotal],
                backgroundColor: [
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(153, 204, 50, 0.2)',
                    'rgba(255, 127, 0, 0.2)',
                    'rgba(79, 47, 79, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 206, 86, 1)',
                    'rgba(153, 204, 50, 1)',
                    'rgba(255, 127, 0, 1)',
                    'rgba(79, 47, 79, 1)'
                ],
                borderWidth: 2
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
};

function GraficoSomaAtivos(dados) {
    var ctx = document.getElementById('somaAtivos').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'horizontalBar',
        data: {
            labels: ['Soma Ativos'],
            datasets: [{
                label: 'Relação',
                data: [dados.valorTotal],
                backgroundColor: [
                    'rgba(255, 127, 0, 0.2)',
                    'rgba(79, 47, 79, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 127, 0, 1)',
                    'rgba(79, 47, 79, 1)'
                ],
                borderWidth: 2
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
    $("#divLoading").hide(400);

};