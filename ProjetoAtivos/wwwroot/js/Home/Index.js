$(document).ready(function () {
    $("#divLoading").show();
    Grafico();
    $('[data-toggle="tooltip"]').tooltip()
});/*
(function ($) {
    var refreshKeyPressed = false;
    var modifierPressed = false;
    var f5key = 116;
    var rkey = 82;
    var modkey = [17, 224, 91, 93];

    // Check for refresh keys
    $(document).bind('keydown', function (evt) {
        // Check for refresh
        if (evt.which == f5key || window.modifierPressed && evt.which == rkey) {
            refreshKeyPressed = true;
        }

        // Check for modifier
        if (modkey.indexOf(evt.which) >= 0) {
            modifierPressed = true;
        }
    });

    // Check for refresh keys
    $(document).bind('keyup', function (evt) {
        // Check undo keys
        if (evt.which == f5key || evt.which == rkey) {
            refreshKeyPressed = false;
        }

        // Check for modifier
        if (modkey.indexOf(evt.which) >= 0) {
            modifierPressed = false;
        }
    });

    $(window).bind('beforeunload', function (event) {
        var message = "not refreshed";

        if (refreshKeyPressed) {
            message = "refreshed";
        }
        else {
            $.ajax({
                type: 'POST',
                url: '/Login/AlterarStatusLogin',
                success: function (result) {

                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });
        }

        event.returnValue = message;
        return message;
    });
}(jQuery));
$(window).bind('beforeunload', function (e) {
    return "If you close the window, your application with still be running. If you wish to stop the application, click 'cancel' and "
});*/
function ViraGrafico() {
    document.querySelector("#mycard").classList.toggle("flip");

    if ($('#front').is(':visible'))
        $('#front').hide();
    else
        $('#front').show();

    if ($('#back').is(':visible'))
        $('#back').hide();
    else
        $('#back').show();
};

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
    $.ajax({
        type: 'POST',
        url: '/Home/Grafico',
        data: {

        },
        success: function (result) { validaLogin(result);

            if (result != null) {
                var i = 0;
                var dados = [];
                while (result[i] != null) {
                    dados.push(result[i]);
                    i++
                }
                GraficoValorAtivoRegional(dados);
                GraficoAtivoRegional(dados);
                CarregarRegionais(dados);
                GraficoImagem(result[i + 1]);
                CardSomaAtivos(result[i + 2]);
                CardStatusTransferencia(result[i + 3]);
                $("#divLoading").hide(0);

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
                $("#divLoading").hide(0);
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(0);
        }
    });
};
function LimparCombo(Name) {
    var select = document.getElementById(Name);
    var length = select.options.length;
    if (length > 1) {
        for (var i = length - 1; i >= 1; i--) {
            select.remove(i);
        }
    }
};
function CarregarRegionais(result) {
    var cbbRegiao = document.getElementById("cbbRegiao");
    if (cbbRegiao != null) {
        for (var i = 0; i < result.length; i++) {
            $('#cbbRegiao').append('<option value="' + result[i].codigo + '">' + result[i].regional + '</option>');
            $('#cbbRegiaoImg').append('<option value="' + result[i].codigo + '">' + result[i].regional + '</option>');
        }
    }
    $('#cbbRegiao').selectpicker('refresh');
    $('#cbbRegiaoImg').selectpicker('refresh');

};
function CarregarFiliais(Codigo) {
    LimparCombo("cbbFilial");

    var cbbFilial = document.getElementById("cbbFilial");

    if (cbbFilial != null) {
        $.ajax({
            type: 'POST',
            url: '/Filial/BuscarFiliais',
            async: false,
            data: { Codigo: Codigo },
            success: function (result) { validaLogin(result);
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        $('#cbbFilial').append('<option value="' + result[i].codigo + '">' + result[i].razao + '</option>');
                    }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(0);
            }
        });

        $('#cbbFilial').selectpicker('refresh');
    }
};

function CarregarFiliaisImg(Codigo) {
    LimparCombo("cbbFilialImg");

    var cbbFilial = document.getElementById("cbbFilialImg");

    if (cbbFilial != null) {
        $.ajax({
            type: 'POST',
            url: '/Filial/BuscarFiliais',
            async: false,
            data: { Codigo: Codigo },
            success: function (result) { validaLogin(result);
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        $('#cbbFilialImg').append('<option value="' + result[i].codigo + '">' + result[i].razao + '</option>');
                    }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(0);
            }
        });

        $('#cbbFilialImg').selectpicker('refresh');
    }
};


function CardStatusTransferencia(dados) {
    var Texto = document.getElementById("txtAguardando");
    Texto.innerHTML = '<i class="fa fa-exclamation"></i> Trasferencias Aguardando: ' + dados.aguardando;

    var Texto2 = document.getElementById("txtAprovado");
    Texto2.innerHTML = '<i class="fa fa-check"></i> Trasferencias Aprovadas: ' + dados.aprovado;
}
function CardSomaAtivos(dados) {
    var Texto = document.getElementById("txtSoma");
    Texto.innerHTML = '<i class="fa fa-dollar-sign"></i> Soma Ativos: ' + dados.valorTotal.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });
    $("#divLoading").hide(0);

};
function CardQuantidadeAtivos(dados) {
    var Texto = document.getElementById("txtQtd");
    Texto.innerHTML = '<i class="fa fa-info"></i> Quantidade Ativos: ' + dados;
};
function MontarLabelRegionalQuantidade(dados) {
    var label = [];
    var Texto = '';


    $.each(dados, function () {
        label.push(this.regional.replace('REGIONAL', '') + ' : ' + this.quantidade)
    });

    return label;
}
function MontarLabelRegionalValor(dados) {
    var label = [];
    var Texto = '';


    $.each(dados, function () {
        label.push(this.regional.replace('REGIONAL', '') + ' : ' + this.soma.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }))
    });

    return label;
};
function MontarDataValorAtivos(dados) {
    var Data = [];
    $.each(dados, function () {
        Data.push(this.soma)
    });
    return Data;
}
function MontarDataAtivos(dados) {
    var Data = [];
    var Soma = 0;
    $.each(dados, function () {
        Data.push(this.quantidade)
        Soma += this.quantidade;
    });
    CardQuantidadeAtivos(Soma);
    return Data;
};
function GraficoValorAtivoRegional(dados) {
    var ctx = document.getElementById('faturamentoregional').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: MontarLabelRegionalValor(dados),
            datasets: [{
                label: ['Ativos'],
                data: MontarDataValorAtivos(dados),
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
                    },
                    stacked: true,
                    display: false
                }]
            },
            responsive: true
        }
    });
}
function GraficoAtivoRegional(dados) {
    var ctx = document.getElementById('ativosregional').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: MontarLabelRegionalQuantidade(dados),
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
                    },
                    display: false
                }]
            },
            responsive: true


        }
    });
};

function GraficoImagem(dados) {
    var ctx = document.getElementById('imagemAtivos').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['C/Imagem - ' + dados.quantidade, 'S/Imagem - ' + dados.quantidadeTotal],
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
                    },
                    display: false
                }]
            },
            responsive: true

        }
    });
};

function GraficoSomaAtivos(dados) {
    var ctx = document.getElementById('somaAtivos').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Soma Ativos'],
            datasets: [{
                label: 'Relação',
                data: [dados.valorTotal.toFixed(2)],
                backgroundColor: [
                    'rgba(255, 127, 0, 0.2)',
                    'rgba(79, 47, 79, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 127, 0, 1)',
                    'rgba(79, 47, 79, 1)'
                ],
                borderWidth: 2,
                minBarLength: 2,
                maxBarThickness: 50,
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    },
                    display: false
                }]
            },
            responsive: true
        }
    });

};
function PreencherTabela(dados) {
    var i = 1;
    var Media = 0;
    var txt = '<thead>\
            <tr class="thead-light">\
                <th scope="col">#ID</th>\
                <th scope="col">Filial</th>\
                <th scope="col">Valor Imobilizado</th>\
                <th scope="col">Ativos</th>\
                    <th scope="col">Media</th>\
            </tr>\
        </thead >\
        <tbody>';
    $.each(dados, function () {
        Media = this.valorAcumulado / this.quantidadeAtivos;
        txt += '<tr><th scope="row">' + i + '</th><td>' + this.razao + '</td><td>' + this.valorAcumulado.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }) + '</td><td>' + this.quantidadeAtivos + '</td><td>'+ Media.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })+'</td></tr>'
        i++;
    });
    txt += '</tbody>';
    $("#tableValorAtivos").html(txt);
    funcaoTable("#tableValorAtivos")
    $("#divLoading").hide(0);

};
function ObterValores() {
    $("#divLoading").show();
    document.getElementById('btnPesquisar').disabled = true;

    Regional = document.getElementById('cbbRegiao').value;
    Filial = document.getElementById('cbbFilial').value;

    $.ajax({
        type: 'POST',
        url: '/Ativo/ObterValores',
        data: { Regional: Regional, Filial: Filial },
        success: function (result) { validaLogin(result);
                PreencherTabela(result);
            document.getElementById('btnPesquisar').disabled = false;
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(0);
            document.getElementById('btnPesquisar').disabled = false;

        }
    });
};
function InserirProgresso(CImagem, SImagem) {
    var Total = CImagem + SImagem;
    var Perc = 100 / Total;
    var PercCImagem = Perc * CImagem;
    var PercSImagem = Perc * SImagem;
    var txt = '<div class="progress">\
        <div class="progress-bar bg-success" role="progressbar" style="width: '+ Perc * CImagem + '%" aria-valuenow="' + PercCImagem.toFixed(2) + '" aria-valuemin="0" aria-valuemax="' + Total + '">' + PercCImagem.toFixed(2) + '%</div>\
        <div class="progress-bar bg-danger" role="progressbar" style="width: '+ Perc * SImagem + '%" aria-valuenow="' + PercSImagem.toFixed(2) + '" aria-valuemin="0" aria-valuemax="' + Total + '">' + PercSImagem.toFixed(2) +'</div>\
               </div ><br>';

    $("#progress").show();
    $("#progress").html(txt);

    var txt2 = '<div class="row"><div class="col-lg-6"><span class="badge badge-success">Com Imagem: ' + CImagem + '</span></div>\
                    <div class="col-lg-6"><span class="badge badge-danger">Sem Imagem: '+ SImagem + '</span></div></div><br>';

    $("#total").show();
    $("#total").html(txt2);
}
function PreencherTabelaImagem(dados) {
    var i = 1;
    var Imagem = 0;
    var SImagem = 0;
    var txt = '<thead>\
            <tr class="thead-light">\
                <th scope="col">#ID</th>\
                <th scope="col">Regional</th>\
                <th scope="col">Filial</th>\
                <th scope="col">Placa</th>\
                <th scope="col">Imagem</th>\
            </tr>\
        </thead >\
        <tbody>';
    $.each(dados, function () {

        if (this.imagem != "") {
            txt += '<tr><th scope="row">' + i + '</th><td>' + this.regional + '</td><td>' + this.filial + '</td><td>' + this.placa + '</td><td><span class="badge badge-success">Imagem</span></td></tr>'
            Imagem++;
        }
        else {
            txt += '<tr><th scope="row">' + i + '</th><td>' + this.regional + '</td><td>' + this.filial + '</td><td>' + this.placa + '</td><td><span class="badge badge-danger">S/Imagem</span></td></tr>'
            SImagem++;
        }

        i++;
    });
    txt += '<tr><td>Com Imagem: ' + Imagem + '</td><td></td><td></td><td></td><td>S/Imagem: ' + SImagem + '</td></td>'
    txt += '</tbody>';

    if ((Imagem + SImagem) > 0)
        InserirProgresso(Imagem, SImagem);

    $("#tableRelatorioImagem").html(txt);
    funcaoTable("#tableRelatorioImagem")
    $("#divLoading").hide(0);
}
function ObterRelatorioImagem() {
    $("#divLoading").show();
    document.getElementById('btnPesquisarImg').disabled = true;

    Regional = document.getElementById('cbbRegiaoImg').value;
    Filial = document.getElementById('cbbFilialImg').value;

    $.ajax({
        type: 'POST',
        url: '/Ativo/ObterRelatorioImagem',
        data: { Regional: Regional, Filial: Filial },
        success: function (result) { validaLogin(result);
            PreencherTabelaImagem(result);
            document.getElementById('btnPesquisarImg').disabled = false;
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(0);
            document.getElementById('btnPesquisar').disabled = false;

        }
    });
};
function funcaoTable(NameTable) {
    if ($.fn.dataTable.isDataTable(NameTable)) {
        $(NameTable).DataTable().destroy();
        $(NameTable).DataTable({
            "columns": [
                { "data": "Id" },
                { "data": "Filial" },
                { "data": "Valor" },
                { "data": "Ativos" },
                { "data": "Media" }
            ],
            dom: 'Bfrtip',
            buttons: [
                {
                    text: 'Imprimir',
                    extend: 'print',
                    exportOptions: { orthogonal: 'export' }
                },
                {
                    text: 'Excel',
                    extend: 'excelHtml5',
                    exportOptions: { orthogonal: 'export' }
                },
                {
                    text: 'Pdf',
                    extend: 'pdfHtml5',
                    exportOptions: { orthogonal: 'export' }
                }
            ],
            "language": {
                "sEmptyTable": "Nenhum registro encontrado",
                "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "_MENU_ resultados por página",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sZeroRecords": "Nenhum registro encontrado",
                "sSearch": "Pesquisar",
                "oPaginate": {
                    "sNext": "Próximo",
                    "sPrevious": "Anterior",
                    "sFirst": "Primeiro",
                    "sLast": "Último"
                }
            },
            "bFilter": false
        });
     
    }
    else {
        $(NameTable).DataTable({
            "columns": [
                { "data": "Id" },
                { "data": "Filial" },
                { "data": "Valor" },
                { "data": "Ativos" },
                { "data": "Media" }
            ],
            dom: 'Bfrtip',
            lengthChange: false,
            buttons: [
                {
                    extend: 'print',
                    text: 'Imprimir <i class="fa fa-print"></i>',
                    titleAttr: 'Imprimir'
                },
                {
                    text: 'Excel <i class="fa fa-file-excel"></i>',
                    titleAttr: 'Excel',
                    extend: 'excelHtml5',
                    exportOptions: { orthogonal: 'export' }
                },
                {

                    text: 'Pdf <i class="fa fa-file-pdf"></i>',
                    titleAttr: 'Pdf',
                    extend: 'pdfHtml5',
                    exportOptions: { orthogonal: 'export' }
                }
            ],
            "language": {
                "sEmptyTable": "Nenhum registro encontrado",
                "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "_MENU_ resultados por página",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sZeroRecords": "Nenhum registro encontrado",
                "sSearch": "Pesquisar",
                "oPaginate": {
                    "sNext": "Próximo",
                    "sPrevious": "Anterior",
                    "sFirst": "Primeiro",
                    "sLast": "Último"
                }
            },
            "bFilter": false
        });
      
    }
};