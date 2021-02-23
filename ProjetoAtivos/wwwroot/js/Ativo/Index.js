
function LimparCombo(Name) {
    var select = document.getElementById(Name);
    var length = select.options.length;
    if (length > 1) {
        for (var i = length - 1; i >= 1; i--) {
            select.remove(i);
        }
    }
};
function CarregarTiposAtivo() {
    var cbbTpAtivo = document.getElementById("cbbTpAtivo");
    if (cbbTpAtivo != null) {
        var Chave = "";
        var Filtro = "Descricao";
        var Ativo = 1;

        $.ajax({
            type: 'POST',
            url: '/TipoAtivo/ObterTiposAtivos',
            async: false,
            data: { Chave: Chave, Filtro: Filtro, Ativo: Ativo },
            success: function (result) {
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {

                        var opt = document.createElement("option");
                        opt.value = result[i].codigo;
                        opt.text = result[i].descricao;
                        cbbTpAtivo.add(opt, cbbTpAtivo.options[i + 1]);
                    }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }
    $('#cbbTpAtivo').selectpicker('refresh');

};


function CarregarRegionaisPesq() {
    LimparCombo("cbbRegiaoPesq");

    var cbbRegional = document.getElementById("cbbRegiaoPesq");

    if (cbbRegional != null) {
        var Chave = "";
        var Filtro = "Descricao";
        var Ativo = 1;

        $.ajax({
            type: 'POST',
            url: '/Regional/ObterRegionais',
            async: false,
            data: { Chave: Chave, Filtro: Filtro, Ativo: Ativo },
            success: function (result) {
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        $('#cbbRegiaoPesq').append('<option value="' + result[i].codigo + '">' + result[i].descricao + '</option>');
                    }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }

    $('#cbbRegiaoPesq').selectpicker('refresh');
}


function CarregarRegionaisAtivo() {
    var regiao = parseInt($("#regional").val());
    var filial = parseInt($("#filial").val());
    var regiaoFilial = parseInt($("#regiaoFilial").val());

    $("#cbbRegional").html('<option value="" selected>Regionais</option>');
    var cbbRegional = document.getElementById("cbbRegional");
    if (cbbRegional != null) {
        var Chave = "";
        var Filtro = "Descricao";
        var Ativo = 1;

        $.ajax({
            type: 'POST',
            url: '/Regional/ObterRegionais',
            async: false,
            data: { Chave: Chave, Filtro: Filtro, Ativo: Ativo },
            success: function (result) {
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {

                        var opt = document.createElement("option");
                        opt.value = result[i].codigo;
                        opt.text = result[i].descricao;
                        cbbRegional.add(opt, cbbRegional.options[i + 1]);
                    }

                    if (filial != 0) {
                        $("#cbbRegional").val(regiaoFilial).change();;
                        $("#cbbRegional").attr('disabled', 'disabled');
                        CarregarFiliaisAtivo(cbbRegional);
                    }
                    else
                        if (regiao != 0) {
                            $("#cbbRegional").val(regiao).change();
                            $("#cbbRegional").attr('disabled', 'disabled');
                            CarregarFiliaisAtivo(cbbRegional);
                        }
                        else {
                            $("#cbbRegional").removeAttr('disabled');
                        }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }
    $('#cbbRegional').selectpicker('refresh');

};

function CarregarFiliaisPesq(regiao) {

    LimparCombo("cbbFilialPesq");

    if (regiao != 0) {

        var cbbFilialPesq = document.getElementById("cbbFilialPesq");
        
        $.ajax({
            type: 'POST',
            url: '/Filial/BuscarFiliais',
            async: false,
            data: { Codigo: regiao },
            success: function (result) {
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {

                        /*var opt = document.createElement("option");
                        opt.value = result[i].codigo;
                        opt.text = result[i].razao;
                        cbbFilialPesq.add(opt, cbbFilialPesq.options[i + 1]);*/
                        $('#cbbFilialPesq').append('<option value="' + result[i].codigo + '">' + result[i].razao + '</option>');

                    }
                    $("#filialPesq").show();
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }
    else {
        LimparCombo("cbbFilialPesq");
    }
    $('#cbbFilialPesq').selectpicker('refresh');
};


function CarregarFiliaisAtivo(Combo) {
    document.getElementById("cbbFilialAtivo").required = true;
    LimparCombo("cbbSalaAtivo");
    var filial = parseInt($("#filial").val());

    var Codigo = Combo.value;
    var cbbFilialAtivo = document.getElementById("cbbFilialAtivo");
    if (cbbFilialAtivo != null) {
        document.getElementById('linhaFilial').style.display = "block ";
        document.getElementById("cbbFilialAtivo").required = true;

        $.ajax({
            type: 'POST',
            url: '/Filial/BuscarFiliais',
            async: false,
            data: { Codigo: Codigo },
            success: function (result) {
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {

                        var opt = document.createElement("option");
                        opt.value = result[i].codigo;
                        opt.text = result[i].razao;
                        cbbFilialAtivo.add(opt, cbbFilialAtivo.options[i + 1]);
                    }

                    if (filial != 0) {
                        $("#cbbFilialAtivo").val(filial).change();
                        $("#cbbFilialAtivo").attr('disabled', 'disabled');
                        BuscarSalasAtivo(cbbFilialAtivo);
                    }
                    else
                        $("#cbbFilialAtivo").removeAttr('disabled');

                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }
    $('#cbbFilialAtivo').selectpicker('refresh');
    $('#cbbSalaAtivo').selectpicker('refresh');
    

};
function BuscarSalasAtivo(Combo) {

    /*if ($("#cbbTpAtivo").val() != 3) {
        document.getElementById("cbbSalaAtivo").required = true;
        */

        var Codigo = Combo.value;
        var cbbSalaAtivo = document.getElementById("cbbSalaAtivo");
        if (cbbSalaAtivo != null) {
            document.getElementById('sala').style.display = "block ";
            document.getElementById("cbbSalaAtivo").required = true;
            $.ajax({
                type: 'POST',
                url: '/Sala/BuscarSalas',
                async: false,
                data: { Codigo: Codigo },
                success: function (result) {
                    if (result != null && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {

                            var opt = document.createElement("option");
                            opt.value = result[i].codigo;
                            opt.text = result[i].descricao;
                            cbbSalaAtivo.add(opt, cbbSalaAtivo.options[i + 1]);
                        }
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(300);
                }
            });

        }
        $('#cbbSalaAtivo').selectpicker('refresh');
        $('#sala').show();
    /*}
    else {
        $('#sala').hide();
    }*/
};

function LimparTabela() {
    if ($.fn.dataTable.isDataTable('#tableAtivo')) {
        $('#tableAtivo').DataTable().destroy();
    }
    $("#tbbAtivo").hide(0);
    $("#tableAtivo tr").remove();
    $("#txtPesquisar").val("");
    $('#cbbRegiaoPesq').selectpicker('val', '');
    $('#cbbFilialPesq').selectpicker('val', '0');
    var Radio = document.getElementsByName("rdAtivo"); Radio[0].checked = true;
};

function LimparCampos() {
    $("#txtId").val("0");
    $("#txtPlaca").val("");
    $("#txtTag").val("");
    $("#cbbEstado").val("");
    $("#txtObservacao").val("");
    $("#txtDescricao").val("");
    $("#txtMarca").val("");
    $("#txtNumSerie").val("");
    //$("#txtValor").val("");
    $("#txtModelo").val("");
    $("#txtQtd").val("0");
    $("#validaPlaca").val("0");
    $('#cbbTpAtivo').selectpicker('val', '');


    $("#modalFotos").hide();

    $("#imagem").html("");
    $("#minhaImagemHiddenAtivo").val("");
    document.getElementById("fuArquivoAtivo").value = "";
    document.getElementById("labelFoto").innerHTML = 'Selecione uma Foto';
    $("#cardRegional").show();
    $("#modalArquivo").show();

    document.getElementById("staticBackdropLabel").innerHTML = "Cadastro de Ativos";

    document.getElementById("fuArquivoAtivo").required = true;

    $("#txtFornecedor").val("");
    $("#txtDataEmissao").val("");
    $("#txtValorNota").val("");
    $("#txtCnpj").val("");
    $("#txtNumeroNota").val("");
    $("#hdAnexo").val("");
    $("#nomeAnexo").val("");
    $('#linkAnexo').attr('href', '');

    $("#anexo").hide();

};
function Rolagem() {
    var $anchor = $(this);
    $('html, body').stop().animate({
        scrollTop: ($($anchor.attr('tbbAtivo')).offset().top)
    }, 1000, 'easeInOutExpo');
};
function GerarRelatorio() {
    var doc = new jsPDF()
    doc.autoTable({
        html: '#tableAtivo'
    })
    doc.save('ativos.pdf');

};
function GerarExcel() {
    var htmls = "";
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><meta charset="UTF-8"><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
    var base64 = function (s) {
        return window.btoa(unescape(encodeURIComponent(s)))
    };

    var format = function (s, c) {
        return s.replace(/{(\w+)}/g, function (m, p) {
            return c[p];
        })
    };

    htmls = document.getElementById('tableAtivo').innerHTML;

    var ctx = {
        worksheet: 'Worksheet',
        table: htmls
    }


    var link = document.createElement("a");
    link.download = "export.xlsx";
    link.href = uri + base64(format(template, ctx));
    link.click();
};


function funcaoTable(NameTable) {
    if ($.fn.dataTable.isDataTable(NameTable)) {
        $(NameTable).DataTable().destroy();
        $(NameTable).DataTable({
            dom: 'Bfrtip',
            lengthMenu: [
                [10, 25, 50, -1],
                ['10 Linhas', '25 Linhas', '50 Linhas', 'Todos']
            ],
            buttons: [
                'pageLength',
                {
                    extend: 'print',
                    text: 'Imprimir <i class="fa fa-print"></i>',
                    titleAttr: 'Imprimir',
                    footer: true
                },
                {
                    text: 'Excel <i class="fa fa-file-excel"></i>',
                    titleAttr: 'Excel',
                    extend: 'excelHtml5',
                    footer: true,
                    exportOptions: { orthogonal: 'export' }
                },
                {

                    text: 'Pdf <i class="fa fa-file-pdf"></i>',
                    titleAttr: 'Pdf',
                    extend: 'pdfHtml5',
                    footer: true,
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
            "bFilter": true,
            columnDefs: [
                {
                    targets: 0,
                    createdCell: function (td, cellData, rowData, row, col) {
                        $(td).css('border-radius', '4px');
                        if (rowData[7] == 1)
                            $(td).css('border-left', '4px solid green');
                        else
                            $(td).css('border-left', '4px solid red');

                    }
                }
            ]
        });
    }
    else {
        $(NameTable).DataTable({
            dom: 'Bfrtip',
            lengthMenu: [
                [10, 25, 50, -1],
                ['10 Linhas', '25 Linhas', '50 Linhas', 'Todos']
            ],
            buttons: [
                'pageLength',
                {
                    extend: 'print',
                    text: 'Imprimir <i class="fa fa-print"></i>',
                    titleAttr: 'Imprimir',
                    footer: true
                },
                {
                    text: 'Excel <i class="fa fa-file-excel"></i>',
                    titleAttr: 'Excel',
                    extend: 'excelHtml5',
                    footer: true,
                    exportOptions: { orthogonal: 'export' }
                },
                {

                    text: 'Pdf <i class="fa fa-file-pdf"></i>',
                    titleAttr: 'Pdf',
                    extend: 'pdfHtml5',
                    footer: true,
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
            "bFilter": true,
            columnDefs: [
                {
                    targets: 0,
                    createdCell: function (td, cellData, rowData, row, col) {
                        $(td).css('border-radius', '4px');
                        if (rowData[7] == 1)
                            $(td).css('border-left', '4px solid green');
                        else
                            $(td).css('border-left', '4px solid red');
                    }
                }
            ]
        });
    }
};
function Status(Ativo) {
    if (Ativo)
        return '<span class="badge badge-success">Ativo</span>';
    else
        return '<span class="badge badge-danger">Inativo</span>';
};
function PreencherTabelaAtivo(dados) {
    var i = 0;
    var ValorAtivo = 0;

    $("#tbbAtivo").show(300);
    var txt = '<thead>\
            <tr class="thead-light">\
                <th scope="col" width="6%">Ativo</th>\
                <th scope="col">Placa</th>\
                <th scope="col">Descrição</th>\
                <th scope="col">Valor</th>\
                <th scope="col">Estado</th>\
                <th scope="col">Filial</th>\
                <th scope="col" style="width: 6%;">Status</th>\
                <th scope="col" style="display:none;"></th>\
                <th scope="col"></th>\
            </tr>\
        </thead >\
        <tbody>';
    $.each(dados, function () {
        if (this.notaFiscal == "") 
            ValorAtivo = this.valorAtivo;
        else
            ValorAtivo = this.valorNota;

        if (this.stAtivo == 1) {
            if (this.imagem != "") {
                txt += '<tr class="galeria" ondblclick="mostraDivVeiculo(false); mostraDivAtivo(); UnlockFields(); Alterar(' + this.codigo + ');"><td onclick="javascript:ObterImagens(' + this.codigo + '); "><img id="minhaImagem' + i + '" src="' + this.imagem + '" class="rounded" alt="..." width=40 height=40></td><td>' + this.placa + '</td><td>' + this.descricao + '</td><td>R$' + ValorAtivo+'</td><td>' + this.estado + '</td><td>' + this.razao + '</td><td>' + Status(this.stAtivo) + '</td><td style="display:none;">1</td><td align="right" class="form-group">'
                txt += '<a role="button" class="btn btn-warning" href="javascript: mostraDivVeiculo(false); mostraDivAtivo(); UnlockFields(); Alterar(' + this.codigo + ');" title="Editar Registro"><i class="fas fa-edit"></i></a>';
                txt += ' <a role="button" class="btn btn-danger" href="javascript:ExcluirLogico(' + this.codigo + ');" title="Excluir Registro"><i class="fas fa-trash"></i></a>';
                txt += ' <a role="button" class="btn btn-success"  href="javascript: BuscarLocalizacao(' + this.codigo + ');" title="Localização Ativo"><i class="fas fa-map-marker"></i></a>&nbsp;';
                txt += '<a role="button" class="btn btn-info" href="javascript: Inventariar(' + this.codigo + ');" title="Inventariar"><i class="fas fa-book"></i></a>';
            }
            else {
                txt += '<tr class="galeria" ondblclick="mostraDivVeiculo(false); mostraDivAtivo();UnlockFields(); Alterar(' + this.codigo + ');"><td><img id="minhaImagem' + i + '" src="" class="rounded" alt="..." width=40 height=40></td><td>' + this.placa + '</td><td>' + this.descricao + '</td><td>R$' + ValorAtivo +'</td><td>' + this.estado + '</td><td>' + this.razao + '</td><td>' + Status(this.stAtivo) + '</td><td style="display:none;">0</td><td align="right" class="form-group">'
                txt += '<a role="button" class="btn btn-warning" href="javascript: mostraDivVeiculo(false); mostraDivAtivo(); UnlockFields(); Alterar(' + this.codigo + ');" title="Editar Registro"><i class="fas fa-edit"></i></a>'
                txt += ' <a role="button" class="btn btn-danger" href="javascript:ExcluirLogico(' + this.codigo + ');" title="Excluir Registro"><i class="fas fa-trash"></i></a>&nbsp;';
                txt += '<a role="button" class="btn btn-info" href="javascript: Inventariar(' + this.codigo + ');" title="Inventariar"><i class="fas fa-book"></i></a>';
            }
        }
        else {
            if (this.imagem != "") {
                txt += '<tr class="galeria" ondblclick="mostraDivVeiculo(false); mostraDivAtivo();UnlockFields(); Alterar(' + this.codigo + ');"><td onclick="javascript:ObterImagens(' + this.codigo + '); "><img id="minhaImagem' + i + '" src="' + this.imagem + '" class="rounded" alt="..." width=40 height=40></td><td>' + this.placa + '</td><td>' + this.descricao + '</td><td>R$' + ValorAtivo +'</td><td>' + this.estado + '</td><td>' + this.razao + '</td><td>' + Status(this.stAtivo) + '</td><td style="display:none;">1</td><td align="right" class="form-group">'
                txt += '<a role="button" class="btn btn-success" href="javascript:Ativar(' + this.codigo + ');" title="Ativar Registro"><i class="fas fa-check"></i></a>';
                txt += ' <a role="button" class="btn btn-success"  href="javascript: BuscarLocalizacao(' + this.codigo + ');" title="Localização Ativo"><i class="fas fa-map-marker"></i></a>&nbsp;';
                txt += '<a role="button" class="btn btn-info" href="javascript: Inventariar(' + this.codigo + ');" title="Inventariar"><i class="fas fa-book"></i></a>';
            }
            else {
                txt += '<tr class="galeria" ondblclick="mostraDivVeiculo(false); mostraDivAtivo();UnlockFields(); Alterar(' + this.codigo + ');"><td><img id="minhaImagem' + i + '" src="" class="rounded" alt="..." width=40 height=40></td><td>' + this.placa + '</td><td>' + this.descricao + '</td><td>R$' + ValorAtivo +'</td><td>' + this.estado + '</td><td>' + this.razao + '</td><td>' + Status(this.stAtivo) + '</td><td style="display:none;">0</td><td align="right" class="form-group">'
                txt += '<a role="button" class="btn btn-success" href="javascript:Ativar(' + this.codigo + ');" title="Ativar Registro"><i class="fas fa-check"></i></a>&nbsp;';
                txt += '<a role="button" class="btn btn-info" href="javascript: Inventariar(' + this.codigo + ');" title="Inventariar"><i class="fas fa-book"></i></a>';

            }
        }
        txt += '</td></tr>';
        i++;
    });
    txt += '</tbody>';
    $("#tableAtivo").html(txt);
    funcaoTable("#tableAtivo");
    Rolagem();
}
function ObterAtivos() {
    $("#divLoading").show(300);
    document.getElementById('btnPesquisar').disabled = true;

    var Chave = $("#txtPesquisar").val();
    var Filtro = document.querySelector('input[name="rdFiltro"]:checked').value;
    var Ativo = document.querySelector('input[name="rdAtivo"]:checked').value;
    var Radio = document.getElementsByName("rdAtivo");
    var regiao = parseInt($("#cbbRegiaoPesq").val());
    var filial = parseInt($("#cbbFilialPesq").val());


    $.ajax({
        type: 'POST',
        url: '/Ativo/ObterAtivos',
        data: { Chave: Chave, Filtro: Filtro, Ativo: Ativo, Regiao: regiao, Filial: filial },
        success: function (result) {
            if (result != null && result.length > 0) {
                PreencherTabelaAtivo(result);
            }
            else {
                if (Ativo == "1") {
                    Texto = "Nenhum Ativo Encontrado!";
                }
                else {
                    Texto = "Nenhum Ativo INATIVO Encontrado!";

                }
                Swal.fire({
                    title: 'Oops...',
                    text: Texto,
                    type: 'error',
                    timer: 5000
                })
                $("#tbbAtivo").hide(0);
                Radio[0].checked = true;
                LimparTabela();
            }
            $("#divLoading").hide(300);
            $("#txtPesquisar").val("");
            document.getElementById('btnPesquisar').disabled = false;

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(300);
            document.getElementById('btnPesquisar').disabled = false;

        }
    });
};
function VerificaConteudoNotaFiscal() {
    var NotaFiscal = document.getElementById('notafiscal').className;
    if (NotaFiscal.indexOf('show') != -1) {// diferente de -1 é encontrado
       document.getElementById("txtNumeroNota").required = false;
        document.getElementById("txtValorNota").required = false;
        document.getElementById("txtDataEmissao").required = false;
        document.getElementById("txtFornecedor").required = false;

        document.getElementById('txtNumeroNota').value = "";
        document.getElementById('txtValorNota').value = "";
        document.getElementById('txtDataEmissao').value = "";
        document.getElementById('txtFornecedor').value = "";
    }
    else {//nota on
        document.getElementById("txtNumeroNota").required = true;
        document.getElementById("txtValorNota").required = true;
        document.getElementById("txtDataEmissao").required = true;
        document.getElementById("txtFornecedor").required = true;
    }
};
function AvaliarValorAtivo() {

    var NotaFiscal = document.getElementById('notafiscal').className;
    if (NotaFiscal.indexOf('show') != -1) {// nota aberta
        document.getElementById('').value = 0; //valor nota
    }
    else {      //nota fechada
        document.getElementById('').value = 0; //valor tipo ativo
    }
}
function GravarAtivo() {
    $("#divLoading").show();
    document.getElementById('btnConfirmar').disabled = true;

    if (document.getElementById('validaPlaca').value == '0') {
        navigator.geolocation.getCurrentPosition(function Responder(position) {
            var Latitude = position.coords.latitude;
            var Longitude = position.coords.longitude;

            var TipoAtivo = $('#cbbTpAtivo').val();
            var Codigo = $('#txtId').val();
            var Regional = $('#cbbRegional').val();
            var Filial = $('#cbbFilialAtivo').val();
            
            var Placa = $('#txtPlaca').val();
            var Tag = $('#txtTag').val();
            var Estado = $('#cbbEstado').val();
            var Observacao = $('#txtObservacao').val();
            var Descricao = $('#txtDescricao').val();            
            
            var NumeroSerie = $('#txtNumSerie').val();
            
            var Valor = document.getElementById('txtValorNota').value.replace(',', '.');

            var CodigoNota = document.getElementById('txtIdNotaFiscal').value;
            var NumeroNota = document.getElementById('txtNumeroNota').value;
            var ValorNota = document.getElementById('txtValorNota').value.replace(',','.');
            var DataEmissao = document.getElementById('txtDataEmissao').value;
            var Fornecedor = document.getElementById('txtFornecedor').value;
            var Cnpj = document.getElementById('txtCnpj').value;
            

            var anexo = $("#hdAnexo").val();
            var nomeAnexo = $("#nomeAnexo").val();            
            
            var VerificaImagem = $("#minhaImagemHiddenAtivo").val();
            if (VerificaImagem != "") {
                var imagem = $("#minhaImagemHiddenAtivo").val();

                /*if (TipoAtivo != 3) {*/

                    var Sala = $('#cbbSalaAtivo').val();
                    var Marca = $('#txtMarca').val();
                    var Modelo = $('#txtModelo').val();

                    $.ajax({
                        type: 'POST',
                        url: '/Ativo/Gravar',
                        data: {
                            Codigo: Codigo, Regional: Regional, Filial: Filial, Sala: Sala, Placa: Placa, Tag: Tag, Estado: Estado, Observacao: Observacao,
                            Descricao: Descricao, TipoAtivo: TipoAtivo, Marca: Marca, NumeroSerie: NumeroSerie, Modelo: Modelo, Valor: Valor, Imagem: imagem, Latitude: Latitude, Longitude: Longitude,
                            CodigoNota: CodigoNota, NumeroNota: NumeroNota, ValorNota: ValorNota, DataEmissao: DataEmissao, Fornecedor: Fornecedor, Cnpj: Cnpj, NomeAnexo: nomeAnexo, Anexo: anexo
                        },
                        success: function (result) {
                            $('#novoAtivo').modal('hide');

                            if (result.length > 0) {
                                Swal.fire({
                                    title: 'Oops...',
                                    type: 'error',
                                    text: result,
                                    timer: 5000
                                })
                            }
                            else {
                                if (Codigo == 0) {
                                    Swal.fire({
                                        title: 'Sucesso',
                                        type: 'success',
                                        text: 'Ativo Gravado com Sucesso',
                                        timer: 5000
                                    })
                                }
                                else {
                                    Swal.fire({
                                        title: 'Sucesso',
                                        type: 'success',
                                        text: 'Ativo Alterado com Sucesso',
                                        timer: 5000
                                    })
                                }
                            }
                            document.getElementById('btnConfirmar').disabled = false;
                            if ($("#cbbRegiaoPesq").val() != "")
                                //ObterAtivos();

                            $("#divLoading").hide();
                        },
                        error: function (XMLHttpRequest, txtStatus, errorThrown) {
                            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                            $("#divLoading").hide(400);
                            document.getElementById('btnConfirmar').disabled = false;
                        }
                    });
               /* }
                else {

                    var cor = $('#txtCor').val();
                    var placaVeiculo = $('#txtPlavaV').val();
                    var crlv = $('#hdCRLV').val();
                    var dut = $('#hdDUT').val();
                    var fipe = $('#codFipe').val(); 
                    Marca = $("#cbbMarcaV option:selected").text(); 
                    Modelo = $("#cbbModeloV option:selected").text() + ' ' + $("#cbbAnoVeiculo option:selected").text();  
                    var modeloV = $("#cbbModeloV").val();
                    //var valor = $('#txtPrecoV').val();

                    $.ajax({
                        type: 'POST',
                        url: '/Ativo/GravarVeiculo',
                        data: {
                            Codigo: Codigo, Regional: Regional, Filial: Filial, Placa: Placa, Tag: Tag, Estado: Estado, Observacao: Observacao,
                            Descricao: Descricao, TipoAtivo: TipoAtivo, Marca: Marca, NumeroSerie: NumeroSerie, Modelo: Modelo, Valor: Valor, Imagem: Imagem, Latitude: Latitude, Longitude: Longitude,
                            CodigoNota: CodigoNota, NumeroNota: NumeroNota, ValorNota: ValorNota, DataEmissao: DataEmissao, Fornecedor: Fornecedor, Cnpj: Cnpj, NomeAnexo: nomeAnexo, Anexo: anexo,
                            Cor: cor, PlacaVeiculo: placaVeiculo, CRLV: crlv, DUT: dut, FIPE: fipe, ModeloV: modeloV
                        },
                        success: function (result) {
                            $('#novoAtivo').modal('hide');

                            if (result.length > 0) {
                                Swal.fire({
                                    title: 'Oops...',
                                    type: 'error',
                                    text: result,
                                    timer: 5000
                                })
                            }
                            else {
                                if (Codigo == 0) {
                                    Swal.fire({
                                        title: 'Sucesso',
                                        type: 'success',
                                        text: 'Ativo Gravado com Sucesso',
                                        timer: 5000
                                    })
                                }
                                else {
                                    Swal.fire({
                                        title: 'Sucesso',
                                        type: 'success',
                                        text: 'Ativo Alterado com Sucesso',
                                        timer: 5000
                                    })
                                }
                            }
                            document.getElementById('btnConfirmar').disabled = false;
                            if ($("#cbbRegiaoPesq").val() != "")
                                ObterAtivos();

                            $("#divLoading").hide();
                        },
                        error: function (XMLHttpRequest, txtStatus, errorThrown) {
                            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                            $("#divLoading").hide(400);
                            document.getElementById('btnConfirmar').disabled = false;
                        }
                    });
                }*/
            }
            else {
                Mensagem("divAlerta", 'Por favor Envie a Imagem');
                document.getElementById('btnConfirmar').disabled = false;
                $("#divLoading").hide();
            }
        });
    }
    else
        document.getElementById('btnConfirmar').disabled = false;
  
};
function ExcluirLogico(Codigo) {
    Swal.fire({
        title: 'Você tem Certeza?',
        text: "Você não poderá reverter isso!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Delete Isso!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: 'POST',
                url: '/Ativo/ExcluirLogico',
                data: {
                    Codigo: Codigo
                },
                success: function (result) {

                    if (result.length > 0) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            type: 'warning',
                            allowEscapeKey: false,
                            allowOutsideClick: false,
                            text: result,
                            timer: 5000
                        })
                        ObterAtivos();
                    }
                    else {
                        Swal.fire({
                            title: 'Sucesso',
                            text: "Ativo Excluido com Sucesso!",
                            type: 'success',
                            allowEscapeKey: false,
                            allowOutsideClick: false,
                            confirmButtonColor: '#3085d6',
                        }).then((result) => {
                            if (result.value) {
                                ObterAtivos();
                            }
                        })
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(400);
                }
            });
        }
    })
};

function Ativar(Codigo) {

    $.ajax({
        type: 'POST',
        url: '/Ativo/Ativar',
        data: {
            Codigo: Codigo
        },
        success: function (result) {

            if (result != null && result.length > 0) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    type: 'error',
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    text: result,
                    timer: 5000
                })
                ObterAtivos();
            }
            else {
                var Radio = document.getElementsByName("rdAtivo"); Radio[0].checked = true;
                Swal.fire({
                    title: 'Sucesso',
                    text: "Ativo Ativado com Sucesso",
                    type: 'success',
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    confirmButtonColor: '#3085d6'
                }).then((result) => {
                    if (result.value) {
                        ObterAtivos();
                    }
                })
            }

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
};

function Alterar(Codigo) {
    $("#divLoading").show();

    LimparCampos();
    document.getElementById("cbbRegional").removeAttribute("required");
    document.getElementById("cbbFilialAtivo").removeAttribute("required");
    document.getElementById("cbbSalaAtivo").removeAttribute("required");
    document.getElementById("fuArquivoAtivo").removeAttribute("required");

    document.getElementById("staticBackdropLabel").innerHTML = "Alteração de Ativos";
    $("#cardRegional").hide();
    //$("#modalArquivo").hide();


    $.ajax({
        type: 'POST',
        url: '/Ativo/BuscarAtivo',
        data: {
            Codigo: Codigo
        },
        async: true,
        success: function (result) {

            if (result != null) {

                $('#novoAtivo').modal('show');

                $("#txtId").val(result.codigo);
                $("#txtIdNotaFiscal").val(result.notaFiscal.codigo);
                $("#txtOperacao").val(1);
                $("#txtPlaca").val(result.placa);
                $("#txtTag").val(result.tag);
                $("#cbbEstado").val(result.estado);
                $("#txtObservacao").val(result.observacao);
                $("#txtDescricao").val(result.descricao);
                $('#cbbTpAtivo').selectpicker('val', result.tpAtivoCodigo);
                $("#txtMarca").val(result.marca);
                $("#txtNumSerie").val(result.numeroSerie);
                $("#txtModelo").val(result.modelo);
                $("#txtCnpj").val(result.notaFiscal.cnpj);
                //$("#txtValor").val(result.valor);

                /*if (result.tpAtivoCodigo == 3 && result.veiculo != null) {
                    $('#sala').hide();
                    $('#marcaMod').hide();
                    $('#marcaModV').show();
                    $('#AnoVeiculo').show();
                    $('#upCRLV').show();
                    $('#upDut').show();

                    $("#txtMarca").removeAttr('required');
                    $("#txtModelo").removeAttr('required');
                    $("#txtNumeroNota").removeAttr('required');
                    $("#txtValorNota").removeAttr('required');
                    $("#txtDataEmissao").removeAttr('required');
                    $("#txtFornecedor").removeAttr('required');
                    $("#txtCnpj").removeAttr('required');

                    $('#txtCor').val(result.veiculo.cor);
                    $('#txtPlavaV').val(result.veiculo.placa);
                    $('#codFipe').val(result.veiculo.fipe.codigo); 

                    var parts = result.veiculo.fipe.codigo.split('/');

                    $('#cbbtpVeiculo').val(parts[0]);
                    buscaMarcas(parts[0], parts, result.veiculo.fipe.modelo);

                    //$('#cbbModeloV').val(result.veiculo.fipe.modelo);
                   

                    $('#cbbMarcaV').val(parts[2]);
                    buscaModelos(parts[2])

                    $('#cbbModeloV').val(parts[3]);
                    buscaVersoes(parts[3])

                    $('#cbbAnoVeiculo').val(parts[4]);    

                    registraFipe(parts[4])
                    

                    if (result.veiculo.crlv != null) {
                        $('#hdCRLV').val(result.veiculo.crlv);
                        $('#CRLV').show();
                        $("#nomeCRLV").attr('src', result.veiculo.crlv); 
                    }

                    if (result.veiculo.dut != null) {
                        $('#hdDUT').val(result.veiculo.dut);
                        $('#DUT').show();
                        $("#nomeDUT").attr('src', result.veiculo.dut);
                    }

                    
                }
                else {
                    $('#sala').show();
                    $('#marcaMod').show();
                    $('#marcaModV').hide();
                    $('#AnoVeiculo').hide();
                    $('#upCRLV').hide();
                    $('#upDut').hide();
                    $('#InfoVeiculo').hide();


                    $("#txtMarca").Attr('required', 'required');
                    $("#txtModelo").Attr('required', 'required');
                    $("#txtNumeroNota").Attr('required', 'required');
                    $("#txtValorNota").Attr('required', 'required');
                    $("#txtDataEmissao").Attr('required', 'required');
                    $("#txtFornecedor").Attr('required', 'required');
                    $("#txtCnpj").Attr('required', 'required');
                }*/

                if (result.imagens != null)
                    MostraImagensAtivo(result.imagens);

                if (result.anexo != null)
                {
                    $("#nomeAnexo").val(result.anexo.nome);
                    $('#linkAnexo').attr('href', '/Ativo/BaixarAnexo/' + result.codigo);
                    $('#hdAnexo').val(result.anexo.base64);
                    $("#anexo").show(300);
                }

                if (result.notaFiscal.codigo > 0) {
                    $("#txtFornecedor").val(result.notaFiscal.fornecedor);
                    document.getElementById('txtDataEmissao').value = ObterDataInput(result.notaFiscal.dataEmissao);
                    $("#txtValorNota").val(result.notaFiscal.valorNota);
                    $("#txtNumeroNota").val(result.notaFiscal.codigoNota);
                    //$("#txtValor").val(result.notaFiscal.valorNota);
                }
                else {
                    $("#txtValorNota").val(result.valor);

                }

                $("#divLoading").hide(400);

            }

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
};
function ObterDataInput(Data) {
    var split = Data.split('-');
    if (split.length > 2) {
        var NovaData = split[0] + '-' + split[1] + '-' + split[2][0] + '' + split[2][1];
        return NovaData;
    }
}
function BuscarLocalizacao(Codigo) {
    $.ajax({
        type: 'POST',
        url: '/Ativo/BuscarLocalizacao',
        data: { Codigo: Codigo },
        async: false,
        success: function (result) {
            if (result != null) {
                Mapa(result.latitude, result.longitude)
            }

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(300);
        }
    });
}
function Mapa(latitude, longitude) {
    $('#modalLocalizacao').modal('show');

    var target = document.querySelector('#map');

    navigator.geolocation.getCurrentPosition(function (position) {

        var coordinate = new google.maps.LatLng(latitude, longitude);

        var optionsMap = {
            center: coordinate,
            zoom: 19,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        var map = new google.maps.Map(target, optionsMap);

        var configMarker = {
            position: coordinate,
            map: map,
            title: "O Ativo Esta Aqui"
        };

        var marker = new google.maps.Marker(configMarker);

    });
};
/*
document.querySelector('.custom-file-input').addEventListener('change', function (e) {
    var fileName = document.getElementById("fuArquivoAtivo").files[0].name;
    var nextSibling = e.target.nextElementSibling;
    nextSibling.innerText = fileName;
});*/

function Data(data) {
    if (data != null && data != "") {
        var partes = data.split('-');
        var dia;
        if (partes.length > 2) {
            dia = '' + partes[2][0];
            dia += '' + partes[2][1];
            var dtFormatada = '' + dia + '/' + partes[1] + '/' + partes[0];
            return dtFormatada;
        }
        return data;

    }
    return 'Data Invalida';
};
function MostraImagensAtivo(imgs) {

    var cont = 0;
    $.each(imgs, function () {
        cont++;

        $("#txtQtd").val(parseInt($("#txtQtd").val()) + 1);

        txr2 = this.foto;

        var imgs = $("#minhaImagemHiddenAtivo").val();

        if (imgs != "")
            imgs += "**Separdor Imagem**";

        imgs += txr2;
        $("#minhaImagemHiddenAtivo").val(imgs);

        var txt = '  <div class="col-lg-2" id="fotos' + cont+ '">\
                                        <div class="form-group">\
                                            <div class="card " style="width: 10rem;">\
                                                <img id="minhaImagem" src="'+ txr2 + '" class="card-img-top" alt="...">\
                                                     <p class="text-muted">Data: '+ Data(this.dataInsercao)+ '</p>\
                                                    <div class="card-body">\
                                                        <button type="button" class="btn btn-outline-danger btn-sm" onclick="javascript: ExcluirFotoAtivo('+ $("#txtQtd").val() + ')"><i class="fas fa-trash"></i></button>\
                                                    </div>\
                                                </div>\
                                            </div>\
                                        </div>';


        $("#fotos").show();
        $("#modalFotos").show();


        $("#imagem").append(txt)

    });




}

function SalvarFotosAtivo() {

    var arquivos = document.getElementById("fuArquivoAtivo");
    if (arquivos.files.length > 0) {
        $("#divLoading").show(0);

        document.getElementById('btnSalvarFotosAtivo').innerHTML = '<div class="spinner-border text-primary" role="status"><span class="sr-only" > Loading...</span></div>';
        var txr2;
        var formData = new FormData();
        formData.append("id", $("#txtId").val());
        formData.append("nome", $("#txtNome").val());
        for (var i = 0; i < arquivos.files.length; i++) {
            if (arquivos.files[i].size > 0) {
                formData.append("arquivo" + i, arquivos.files[i]);
            }
        }

        $.ajax({
            type: 'POST',
            url: '/Ativo/ReceberDados',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                var txt = "";
                $.each(response, function () {
                    if (this.id >= 0) {
                        $("#txtQtd").val(parseInt($("#txtQtd").val()) + 1);

                        txr2 = 'data:image/jpg;base64, ' + this.dados;

                        var imgs = $("#minhaImagemHiddenAtivo").val();

                        if (imgs != "")
                            imgs += "**Separdor Imagem**";

                        imgs += txr2;

                        $("#minhaImagemHiddenAtivo").val(imgs);

                        var txt = '  <div class="col-lg-2" id="fotos' + $("#txtQtd").val() + '">\
                                        <div class="form-group">\
                                            <div class="card " style="width: 10rem;">\
                                                <img id="minhaImagem" src="'+ txr2 + '" class="card-img-top" alt="...">\
                                                    <div class="card-body">\
                                                        <button type="button" class="btn btn-outline-danger btn-sm" onclick="javascript: ExcluirFotoAtivo('+ $("#txtQtd").val() + ')"><i class="fas fa-trash"></i></button>\
                                                    </div>\
                                                </div>\
                                            </div>\
                                        </div>';
                        $("#fotos").show();
                        $("#modalFotos").show();


                        $("#imagem").append(txt)
                        //document.getElementById("imagem").innerHTML = txt;

                    }
                    if (this.id == -1) {
                        Mensagem("divAlerta", this.dados);
                    }

                    if (this.id == -2) {
                        Mensagem("divAlerta", this.dados);
                    }

                    document.getElementById('btnSalvarFotosAtivo').innerHTML = 'Adicionar';
                    $("#divLoading").hide(2000);

                });
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(2000);
            }
        });
    }
    else {
        Mensagem("divAlerta", 'Selecione um arquivo!');
    }
    $("#divLoading").hide(2000);

};
function ExcluirFotoAtivo(Codigo) {   
    var Foto = document.getElementById('fotos' + Codigo);

    if (Foto != null) {
        Foto.remove();
        $("#txtQtd").val(parseInt($("#txtQtd").val()) - 1);

        document.getElementById("fuArquivoAtivo").value = "";
        document.getElementById("labelFoto").innerHTML = 'Selecione uma Foto';

        var bases = $("#minhaImagemHiddenAtivo").val().split("**Separdor Imagem**");
        var novo = "";

        for (var i = 0; i < bases.length-1; i++) {
            if (i != Codigo - 1) {

                novo += bases[i];

                if ($("#txtQtd").val() > 1)
                    novo+= "**Separdor Imagem**";
            }
        }

        if (bases.length - 1 != Codigo - 1)
            novo += bases[bases.length - 1];

        $("#minhaImagemHiddenAtivo").val(novo);
    }
    var Imagem = document.getElementById('imagem');

    if (Imagem.childElementCount == 0)
        $("#modalFotos").hide();
};
function Mensagem(div, msg) {
    $("#" + div).html(msg);
    $("#" + div).show(300);
    $("#" + div).delay(6000);
    $("#" + div).hide(300);
};

function UnlockFields() {
    document.getElementById('txtTag').disabled = false;
    document.getElementById('txtObservacao').disabled = false;
    document.getElementById('txtDescricao').disabled = false;
    $("#cbbTpAtivo").attr("disabled", false);
    $('.selectpicker').selectpicker('refresh');

    document.getElementById('txtMarca').disabled = false;
    document.getElementById('txtNumSerie').disabled = false;
    document.getElementById('txtModelo').disabled = false;
    document.getElementById('fuArquivoAtivo').disabled = false;
    document.getElementById('cbbEstado').disabled = false;
    document.getElementById('cbbTpAtivo').disabled = false;
};

function LockFields() {
    document.getElementById('txtTag').disabled = true;
    document.getElementById('txtObservacao').disabled = true;
    document.getElementById('txtDescricao').disabled = true;
    document.getElementById('cbbEstado').disabled = true;
    document.getElementById('txtMarca').disabled = true;
    document.getElementById('txtNumSerie').disabled = true;
    document.getElementById('txtModelo').disabled = true;
    document.getElementById('cbbTpAtivo').disabled = true;
    //document.getElementById('txtValor').disabled = true;
    document.getElementById('fuArquivoAtivo').disabled = true;
    //$("#cbbTpAtivo").attr("disabled", true);
    $('.selectpicker').selectpicker('refresh');

};

function ValidarPlaca() {
    $("#divLoading").show();

    var Placa = document.getElementById('txtPlaca').value;
    var txt = "";
    var status = "";
    $.ajax({
        type: 'POST',
        url: '/Ativo/ObterAtivosPlaca',
        data: { Placa: Placa },
        async: false,
        success: function (result) {
            if (result.length > 0) {
                LockFields();
                for (var i = 0; i < result.length; i++) {
                    if (result[i].status)
                        status = '<span class="badge badge-success">Ativo</span>';
                    else
                        status = '<span class="badge badge-danger">Inativo</span>';

                    txt += 'Placa Ja Cadastrada - Regional: <b>' + result[i].regional + '</b> - Filial: <b>' + result[i].filial + '</b> - Status: ' + status+'<br>'
                }
                Mensagem("divAlertaPlaca", txt);
                document.getElementById('validaPlaca').value = "1";
            }
            else {
                UnlockFields();
                document.getElementById('validaPlaca').value = "0";
                $("#txtTag").focus();
            }
            $("#divLoading").hide(0);

            
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(300);
        }
    });
};

function PreencherValorAtivo(Combo) {
    var Codigo = Combo.value;

    /*if (Codigo == 3) {
        $('#sala').hide();
        $('#marcaMod').hide();
        $('#marcaModV').show();
        $('#AnoVeiculo').show();
        $('#upCRLV').show();
        $('#upDut').show();
        
        $("#txtMarca").removeAttr('required');
        $("#txtModelo").removeAttr('required');
        $("#txtNumeroNota").removeAttr('required');
        $("#txtValorNota").removeAttr('required');
        $("#txtDataEmissao").removeAttr('required');
        $("#txtFornecedor").removeAttr('required');
        $("#txtCnpj").removeAttr('required');

    }
    else {
        $('#sala').show();
        $('#marcaMod').show();
        $('#marcaModV').hide();
        $('#AnoVeiculo').hide();
        $('#upCRLV').hide();
        $('#upDut').hide();
        $('#InfoVeiculo').hide();

        
        $("#txtMarca").Attr('required', 'required');
        $("#txtModelo").Attr('required', 'required');
        $("#txtNumeroNota").Attr('required', 'required');
        $("#txtValorNota").Attr('required', 'required');
        $("#txtDataEmissao").Attr('required', 'required');
        $("#txtFornecedor").Attr('required', 'required');
        $("#txtCnpj").Attr('required', 'required');
    }*/

    $.ajax({
        type: 'POST',
        url: '/TipoAtivo/BuscarTipoAtivo',
        data: { Codigo: Codigo },
        async: false,
        success: function (result) {
            if (result != null) {
                $("#txtValorNota").val(result.valor);
            }
            
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(300);
        }
    });
};

function ModalTipoAtivo() {
    $('#novoAtivo').modal('hide');
    $('#novaTipoAtivo').modal('show');
}
function CancelarTipoAtivo() {
    $('#novaTipoAtivo').modal('hide');
    $('#novoAtivo').modal('show');
}
function LimparCampoTipoAtivo() {
    $("#txtIdTpAtivo").val("0");
    $("#txtDescricaoTpAtivo").val("");
    $("#txtValorTpAtivo").val("");
}
function GravarTipoAtivo() {
    $("#divLoading").show();

    var Codigo = $("#txtIdTpAtivo").val();
    var Descricao = $("#txtDescricaoTpAtivo").val();
    var Valor = $("#txtValorTpAtivo").val();
    var StAtivo = $('#cbAtivo').is(':checked');

    $.ajax({
        type: 'POST',
        url: '/TipoAtivo/Gravar',
        data: {
            Codigo: Codigo, Descricao: Descricao, Valor: Valor, StAtivo: StAtivo
        },
        success: function (result) {
            $('#novaTipoAtivo').modal('hide');

            if (result.length > 0) {
                Swal.fire({
                    title: 'Oops',
                    text: result,
                    type: 'error',
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    confirmButtonColor: '#3085d6',
                }).then((result) => {
                    if (result.value) {
                        $('#novoAtivo').modal('show');
                    }
                })
            }
            else {
                Swal.fire({
                    title: 'Sucesso',
                    type: 'success',
                    text: 'Registro Gravado com Sucesso',
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    confirmButtonColor: '#3085d6',
                }).then((result) => {
                    if (result.value) {
                        $('#novoAtivo').modal('show');
                        LimparCombo("cbbTpAtivo");
                        CarregarTiposAtivo();
                    }
                })

                $("#divLoading").hide(400);
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
};

function AlterarRequeridoNome() {
    document.getElementById("cbbRegiaoPesq").required = true;
    document.getElementById("cbbRegiaoPesq").required = true;
    document.getElementById("txtPesquisar").required = false;
};

function AlterarRequeridoPlaca() {
    $('#cbbRegiaoPesq').selectpicker('val', '');
    $('#cbbFilialPesq').selectpicker('val', '0');

    document.getElementById("cbbRegiaoPesq").required = false;
    document.getElementById("cbbRegiaoPesq").required = false;
    document.getElementById("txtPesquisar").required = true;
};
function ObterImagens(Codigo) {
    $("#divLoading").show();

    $.ajax({
        type: 'POST',
        url: '/Ativo/ObterImagens',
        data: {
            Codigo: Codigo
        },
        success: function (result) {
            if (result.length > 0) {
                $('#galeria').modal('show');
                MontarGaleriaAtivo(result);
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
};

function MontarGaleriaAtivo(dados) {
    var Header = '<li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>';
    var Corpo = '<div class="carousel-item active"><img src="' + dados[0].imagem + '" class="img-fluid d-block" style="height:500px; width:500px; margin:0 auto" alt="..."><div class="carousel-caption d-none d-md-block"><h5>Data Inserção: ' + Data(dados[0].dataInsercao) + '</h5></div></div>';

    for (var i = 1; i < dados.length; i++) {
        Header += '<li data-target="#carouselExampleIndicators" data-slide-to="' + i + '"></li>';
        Corpo += '<div class="carousel-item"><img src="' + dados[i].imagem + '" class="img-fluid d-block" style="height:500px; width:500px; margin:0 auto" alt="..."><div class="carousel-caption d-none d-md-block"><h5>Data Inserção: ' + Data(dados[i    ].dataInsercao) + '</h5></div></div>';
    };

    $("#headerGaleria").html(Header);
    $("#corpoGaleria").html(Corpo);

    $("#divLoading").hide(400);
};

function LimparAnexo(sulf = "Anexo", dest = "anexo", button ="btnSalvarDoc") {
    $("#hd" + sulf).val('');
    $("#nome" + sulf).val('');

    $('#link' + sulf).attr('href', '' );

    $("#" + button).html('Anexar');

    $("#" + dest).hide();
}

function SalvarAnexo(input = "fuDoc", button = 'btnSalvarDoc', sulf = 'Anexo', dest ="anexo", apenaImagens = false) {

    var arquivos = document.getElementById(input);
    if (arquivos.files.length > 0) {
        $("#divLoading").show(0);

        document.getElementById(button).innerHTML = '<div class="spinner-border text-primary" role="status"><span class="sr-only" > Loading...</span></div>';
        var txr2;
        var formData = new FormData();
        formData.append("id", $("#txtId").val());
        formData.append("nome", $("#txtNome").val());

        if (apenaImagens)
            formData.append("apenasImagens", true);

        for (var i = 0; i < arquivos.files.length; i++) {
            if (arquivos.files[i].size > 0) {
                formData.append("arquivo" + i, arquivos.files[i]);
            }
        }

        $.ajax({
            type: 'POST',
            url: '/Ativo/ReceberAnexo',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                var txt = "";
                $.each(response, function () {
                    if (this.id >= 0) {
                        

                        $("#hd" + sulf).val(this.dados);

                        if (apenaImagens)
                            $("#nome" + sulf).attr('src', this.dados)
                        else
                            $("#nome" + sulf).val(this.nome);
                        
                        
                        $("#" + dest).show(300);
                        document.getElementById(button).innerHTML = 'Substituir';

                    }
                    if (this.id == -1) {
                        Mensagem("divAlerta" + sulf, this.dados);
                    }

                    if (this.id == -2) {
                        Mensagem("divAlerta" + sulf, this.dados);
                    }

                    
                    $("#divLoading").hide(2000);

                });
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(2000);
            }
        });
    }
    else {
        Mensagem("divAlerta" + sulf, 'Selecione um arquivo!');
    }
    $("#divLoading").hide(2000);

};

function buscaMarcas(x, parts = null, mod = null) {

    if (x != "") {
        $.ajax({
            type: 'GET',
            url: 'https://fipeapi.appspot.com/api/1/'+x+'/marcas.json',
            dataType: 'json',
           
            success: function (response) {

                var txt = '';
                $.each(response, function () {

                    txt += '<option value="' + this.id + '">' + this.fipe_name+'</option>';

                });

                $("#cbbMarcaV").append(txt);
                $("#cbbMarcaV").removeAttr('disabled');

                $("#cbbModeloV").attr('disabled', 'disabled');
                $("#cbbAnoVeiculo").attr('disabled', 'disabled');

                $("#cbbModeloV").html('<option value="" selected>Modelo do Veículo</option>');
                $("#cbbAnoVeiculo").html('<option value="" selected>Versão Veículo</option>');

                $("#cbbMarcaV").val('');

                if (parts != null) {
                    $('#cbbMarcaV').val(parts[2]);
                    buscaModelos(parts[2], parts, mod);
                }

                $('#cbbModeloV').selectpicker('refresh');
                $('#cbbAnoVeiculo').selectpicker('refresh');

                
                $('#cbbMarcaV').selectpicker('refresh');

                
                
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(2000);
            }
        });

    }
}

function buscaModelos(x, parts = null, mod = null) {

    var tipo = $("#cbbtpVeiculo").val();

    if (x != "") {
        $.ajax({
            type: 'GET',
            url: 'https://fipeapi.appspot.com/api/1/' + tipo+'/veiculos/'+x+'.json',
            dataType: 'json',

            success: function (response) {

                var txt = '';
                $.each(response, function () {

                    txt += '<option value="' + this.id + '">' + this.fipe_name + '</option>';

                });

                $("#cbbModeloV").append(txt);
                $("#cbbModeloV").removeAttr('disabled');

                
                $("#cbbAnoVeiculo").attr('disabled', 'disabled');
               

                $("#cbbAnoVeiculo").html('<option value="" selected>Versão Veículo</option>');


                if (parts != null) {
                    $('#cbbModeloV').val(parts[3]);
                    buscaVersoes(parts[3], parts, mod);
                }

                $('#cbbAnoVeiculo').selectpicker('refresh');

                $('#cbbModeloV').selectpicker('refresh');
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(2000);
            }
        });

    }

}

function buscaVersoes(x, parts = null, mod = null) {

    var tipo = $("#cbbtpVeiculo").val();
    var marca = $("#cbbMarcaV").val();

    if (x != "") {
        $.ajax({
            type: 'GET',
            url: 'https://fipeapi.appspot.com/api/1/'+tipo+'/veiculo/'+marca+'/'+x+'.json',
            dataType: 'json',

            success: function (response) {

                var txt = '';
                $.each(response, function () {

                    txt += '<option value="' + this.id + '">' + this.name + '</option>';

                });

                $("#cbbAnoVeiculo").append(txt);
                $("#cbbAnoVeiculo").removeAttr('disabled');

                if (parts != null) {
                    $('#cbbAnoVeiculo').val(parts[4]);

                    registraFipe(parts[4], parts, mod);
                }

                $('#cbbAnoVeiculo').selectpicker('refresh');
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(2000);
            }
        });

    }

}

function registraFipe(x, parts = null, mod = null) {

    if (x != '') {
        var tipo = $("#cbbtpVeiculo").val();
        var marca = $("#cbbMarcaV").val();
        var modelo = $("#cbbModeloV").val();

        if (parts != null) {
            tipo = parts[0];
            marca = parts[2];
            modelo = parts[3];
            x = parts[4];
        }

        $.ajax({
            type: 'GET',
            url: 'https://fipeapi.appspot.com/api/1/'+tipo+'/veiculo/'+marca+'/'+modelo+'/'+x+'.json',
            dataType: 'json',

            success: function (response) {

                var preco = response.preco;
                $("#txtPrecoV").val(preco);

                var cod = tipo + '/veiculo/' + marca + '/' + response.fipe_codigo + '/' + x;
                $("#codFipe").val(cod)

                $('#InfoVeiculo').show();

                $("#txtCor").removeAttr('disabled');
                $("#txtPlavaV").removeAttr('disabled');

                $('#cbbtpVeiculo').val(parts[0]);
                $('#cbbMarcaV').val(parts[2]);
                $("#cbbModeloV").val(mod);
                $('#cbbAnoVeiculo').val(parts[4]);  

                $('#cbbtpVeiculo').selectpicker('refresh');
                $('#cbbMarcaV').selectpicker('refresh');
                $('#cbbModeloV').selectpicker('refresh');
                $('#cbbAnoVeiculo').selectpicker('refresh');


            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(2000);
            }
        });
    }
}


function SalvarFotosAtivoInventario() {

    var arquivos = document.getElementById("fuArquivoAtivoInventario");
    if (arquivos.files.length > 0) {
        $("#divLoading").show(0);

        //document.getElementById('btnSalvarFotosAtivoInventario').innerHTML = '<div class="spinner-border text-primary" role="status"><span class="sr-only" > Loading...</span></div>';
        var txr2;
        var formData = new FormData();
        formData.append("id", $("#txtIdInventario").val());
        formData.append("nome", $("#txtNomeInventario").val());
        for (var i = 0; i < arquivos.files.length; i++) {
            if (arquivos.files[i].size > 0) {
                formData.append("arquivo" + i, arquivos.files[i]);
            }
        }

        $.ajax({
            type: 'POST',
            url: '/Ativo/ReceberDados',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                var txt = "";
                $.each(response, function () {
                    if (this.id >= 0) {
                        $("#txtQtdInventario").val(parseInt($("#txtQtdInventario").val()) + 1);

                        txr2 = 'data:image/jpg;base64, ' + this.dados;

                        var imgs = $("#minhaImagemHiddenInventario").val();

                        if (imgs != "")
                            imgs += "**Separdor Imagem**";

                        imgs += txr2;

                        $("#minhaImagemHiddenInventario").val(imgs);

                        var txt = '  <div class="col-lg-2" id="fotosInventario' + $("#txtQtdInventario").val() + '">\
                                        <div class="form-group">\
                                            <div class="card " style="width: 10rem;">\
                                                <img id="minhaImagemInventario" src="'+ txr2 + '" class="card-img-top" alt="...">\
                                                    <div class="card-body">\
                                                        <button type="button" class="btn btn-outline-danger btn-sm" onclick="javascript: ExcluirFotoAtivoInventario('+ $("#txtQtdInventario").val() + ')"><i class="fas fa-trash"></i></button>\
                                                    </div>\
                                                </div>\
                                            </div>\
                                        </div>';
                        $("#fotosInventario").show();
                        $("#modalFotosInventario").show();


                        $("#imagemInventario").append(txt)
                        //document.getElementById("imagem").innerHTML = txt;
                        $('#fuArquivoAtivoInventario').attr('disabled', 'disabled');
                    }
                    if (this.id == -1) {
                        Mensagem("divAlertaInventario", this.dados);
                    }

                    if (this.id == -2) {
                        Mensagem("divAlertaInventario", this.dados);
                    }

                    //document.getElementById('btnSalvarFotosAtivoInventario').innerHTML = 'Adicionar';
                    $("#divLoading").hide(2000);

                });
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(2000);
            }
        });
    }
    else {
        Mensagem("divAlerta", 'Selecione um arquivo!');
    }
    $("#divLoading").hide(2000);

};

function ExcluirFotoAtivoInventario(Codigo) {
    var Foto = document.getElementById('fotosInventario' + Codigo);

    if (Foto != null) {
        Foto.remove();
        $("#txtQtdInventario").val(parseInt($("#txtQtdInventario").val()) - 1);

        document.getElementById("fuArquivoAtivoInventario").value = "";
        document.getElementById("labelFotoInventario").innerHTML = 'Selecione uma Foto';

        var bases = $("#minhaImagemHiddenInventario").val().split("**Separdor Imagem**");
        var novo = "";

        for (var i = 0; i < bases.length - 1; i++) {
            if (i != Codigo - 1) {

                novo += bases[i];

                if ($("#txtQtdInventario").val() > 1)
                    novo += "**Separdor Imagem**";
            }
        }

        if (bases.length - 1 != Codigo - 1)
            novo += bases[bases.length - 1];

        $("#minhaImagemHiddenInventario").val(novo);
    }
    var Imagem = document.getElementById('imagemInventario');

    if (Imagem.childElementCount == 0)
        $("#modalFotosInventario").hide();

    $('#fuArquivoAtivoInventario').removeAttr('disabled');
};

function Inventariar(x) {
    $('#idAtivoInventario').val(x);
    $('#inventario').modal('show');
}

function GravarInventario() {
    $("#divLoading").show();

    navigator.geolocation.getCurrentPosition(function Responder(position) {
        var Latitude = position.coords.latitude;
        var Longitude = position.coords.longitude;

        var ativo = $('#idAtivoInventario').val();
        var obs = $('#txtObservacaoInv').val();

        var VerificaImagem = $('#minhaImagemHiddenInventario').val();
        if (VerificaImagem != "") {
            var Imagem = $('#minhaImagemHiddenInventario').val();

            $.ajax({
                type: 'POST',
                url: '/Ativo/Inventariar',
                data: {
                    Codigo: ativo, Observacao: obs, Imagem: Imagem, Latitude: Latitude, Longitude: Longitude
                },
                success: function (result) {
                    $('#inventario').modal('hide');

                    if (result.length > 0) {
                        Swal.fire({
                            title: 'Oops...',
                            type: 'error',
                            text: result,
                            timer: 5000
                        })
                    }
                    else {
                       
                            Swal.fire({
                                title: 'Sucesso',
                                type: 'success',
                                text: 'Ativo Inventariado com Sucesso',
                                timer: 5000
                            })
                        
                    }                   

                    $("#divLoading").hide();
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(400);
                }
            });
        }
        else {
            Mensagem("divAlertaInventario", 'Por favor Envie a Imagem');
            $("#divLoading").hide();
        }

    });
}

function mostraDivAtivo(mostrar = true) {

    let txt = '<div class="modal-dialog modal-xl" role ="document">\
            <div class="modal-content">\
                <div class="modal-header">\
                    <h5 class="modal-title" id="staticBackdropLabel">Cadastrar Ativos</h5>\
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">\
                        <span aria-hidden="true">&times;</span>\
                    </button>\
                </div>\
                <div class="modal-body">\
                    <div class="container-fluid">\
                        <form name="formRegional" id="formRegional" action="javascript: GravarAtivo();" method="post">\
                            <input type="hidden" id="regional" value="@Context.Request.Cookies[" Regiao"].ToString()" />\
                            <input type="hidden" id="filial" value="@Context.Request.Cookies[" Filial"].ToString()" />\
                            <input type="hidden" id="regiaoFilial" value="@Context.Request.Cookies[" RegiaoFilial"].ToString()" />\
                            <input type="hidden" id="minhaImagemHiddenAtivo" name="minhaImagemHiddenAtivo" value="" />\
                            <input type="hidden" id="validaPlaca" name="validaPlaca" value="0" />\
                            <div class="form-group">\
                                <input type="hidden" name="txtId" id="txtId" value="0" />\
                                <div class="card mb-4 shadow p-3 mb-5 bg-white rounded" id="cardRegional">\
                                    <div class="card-header">\
                                        <i class="fa fa-table"></i>\
                                        <span><b>Regionais, Filiais e Salas</b></span>\
                                    </div>\
                                    <div class="card-body">\
                                        <div class="row">\
                                            <div class="col-lg-4">\
                                                <div class="form-group">\
                                                    <label for="cbbRegional">Regional <label style="color:red">*</label></label>\
                                                    <div class="input-group">\
                                                        <div class="input-group-append">\
                                                            <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                        </div>\
                                                        <select id="cbbRegional" name="cbbRegional" class="form-control selectpicker" data-live-search="true" required onchange="javascript:LimparCombo(\'cbbFilialAtivo\');LimparCombo(\'cbbSalaAtivo\'); CarregarFiliaisAtivo(this); ">\
                                                            <option value="" selected>Regionais</option>\
                                                        </select>\
                                                    </div>\
                                                </div>\
                                            </div>\
                                            <div class="col-lg-4" id="linhaFilial" style="display:none">\
                                                <div class="form-group">\
                                                    <label for="\
                                                           ">Filial <label style="color:red">*</label></label>\
                                                    <div class="input-group">\
                                                        <div class="input-group-append">\
                                                            <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                        </div>\
                                                        <select id="cbbFilialAtivo" name="cbbFilialAtivo" class="form-control selectpicker" data-live-search="true" onchange="javascript:LimparCombo(\'cbbSalaAtivo\'); BuscarSalasAtivo(this);">\
                                                            <option value="" selected>Filiais da Regional</option>\
                                                        </select>\
                                                    </div>\
                                                </div>\
                                            </div>\
                                            <div class="col-lg-4" id="sala" style="display:none">\
                                                <div class="form-group">\
                                                    <label for="cbbSala">Local <label style="color:red">*</label></label>\
                                                    <div class="input-group">\
                                                        <div class="input-group-append">\
                                                            <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                        </div>\
                                                        <select id="cbbSalaAtivo" name="cbbSalaAtivo" class="form-control selectpicker" data-live-search="true">\
                                                            <option value="" selected>Salas da Filial</option>\
                                                        </select>\
                                                    </div>\
                                                </div>\
                                            </div>\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class="row">\
                                    <div id="divAlertaPlaca" class="alert alert-danger col-lg-8" role="alert" style="display:none"></div>\
                                </div>\
                                <input type="hidden" name="txtIdNotaFiscal" id="txtIdNotaFiscal" value="0" />\
                                <div class="card card-body" style="border-radius: 4px; border-left: 4px solid blue;">\
                                    <div class="d-sm-flex align-items-center justify-content-between mb-0">\
                                        <h1 class="h6 mb-0 text-gray-800">Dados Fiscais</h1>\
                                    </div>\
                                    <hr />\
                                    <div class="row">\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtNumeroNota">Nº Nota <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="number" name="txtNumeroNota" id="txtNumeroNota" autocomplete="off" maxlength="30" required />\
                                                </div>\
                                            </div>\
                                        </div>\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtValorNota">Valor <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-dollar-sign"></i></div>\
                                                    </div>\
                                                    <input class="form-control money" type="text" name="txtValorNota" id="txtValorNota" autocomplete="off" maxlength="30" required />\
                                                </div>\
                                            </div>\
                                        </div>\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtDataEmissao">Data Emissão <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-dollar-sign"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="date" name="txtDataEmissao" id="txtDataEmissao" autocomplete="off" maxlength="30" required />\
                                                </div>\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row">\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtFornecedor">Fornecedor <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-dollar-sign"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="text" name="txtFornecedor" id="txtFornecedor" autocomplete="off" maxlength="30" required />\
                                                </div>\
                                            </div>\
                                        </div>\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtCnpj">CNPJ <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="text" name="txtCnpj" id="txtCnpj" autocomplete="off" maxlength="30" required data-mask="00.000.000/0000-00" />\
                                                </div>\
                                            </div>\
                                        </div>\
                                        <div class="col-lg-6">\
                                            <div class="form-group">\
                                                <label for="fuDoc">Documento</label>\
                                                <div class="input-group">\
                                                    <div class="custom-file" id="customFile" lang="pt-br">\
                                                        <input type="file" class="custom-file-input" id="fuDoc" aria-describedby="fileHelp" onchange="SalvarAnexo()">\
                                                            <label class="custom-file-label" for="fuDoc" id="labelDoc">\
                                                                Selecione o Documento\
                                                        </label>\
                                                    </div>\
                                                        <div class="input-group-append">\
                                                            <button class="btn btn-success" type="button" id="btnSalvarDoc" onclick="javascript: SalvarAnexo();">Anexar</button>\
                                                        </div>\
                                                    </div>\
                                                </div>\
                                            </div>\
\
                                            <div class="col-lg-6" id="anexo" style="display:none">\
                                                <div class="form-group">\
                                                    <label for="nomeAnexo" style="color:white">.</label>\
                                                    <input type="hidden" id="hdAnexo" name="hdAnexo" value="" />\
\
                                                    <div class="input-group-append">\
                                                        <a id="linkAnexo" href="#" target="_blank" class="stretched-link" style="width: 100%;">\
                                                            <input class="form-control" type="text" id="nomeAnexo" disabled />\
                                                        </a>\
                                                        <button class="btn btn-danger" type="button" onclick="javascript: LimparAnexo();">X</button>\
                                                    </div>\
                                                </div>\
                                            </div>\
\
                                            <div id="divAlertaAnexo" class="alert alert-danger col-lg-6" role="alert" style="display:none"></div>\
\
                                        </div>\
                                    </div>\
                                    <div class="row">\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtPlaca">Placa <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="number" name="txtPlaca" id="txtPlaca" required autocomplete="off" maxlength="30" onchange="javascript: ValidarPlaca(this);" />\
                                                </div>\
                                            </div>\
                                        </div>\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtTag">Tag </label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="text" name="txtTag" id="txtTag" autocomplete="off" maxlength="30" disabled />\
                                                </div>\
\
                                            </div>\
                                        </div>\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="cbbEstado">Estado <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-map"></i></div>\
                                                    </div>\
                                                    <select id="cbbEstado" name="cbbEstado" class="form-control" required>\
                                                        <option value="" selected>Estado</option>\
                                                        <option value="Novo" selected>Novo</option>\
                                                        <option value="Usado">Usado</option>\
                                                    </select>\
                                                </div>\
                                            </div>\
                                        </div>\
\
                                    </div>\
\
                                    <div class="row">\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="cbbTpAtivo">Tipo Ativo <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <select id="cbbTpAtivo" name="cbbTpAtivo" class="form-control selectpicker" data-live-search="true" required onchange="PreencherValorAtivo(this);">\
                                                        <option value="" selected>Tipo Ativo</option>\
                                                    </select>\
                                                </div>\
                                            </div>\
                                        </div>\
\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtDescricao">Descricao <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="text" name="txtDescricao" id="txtDescricao" required autocomplete="off" maxlength="30" disabled />\
                                                </div>\
\
                                            </div>\
                                        </div>\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtNumSerie">Numero de Serie <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="text" name="txtNumSerie" id="txtNumSerie" required autocomplete="off" maxlength="30" disabled />\
                                                </div>\
\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row" id="marcaMod">\
\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtMarca">Marca <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="text" name="txtMarca" id="txtMarca" required autocomplete="off" maxlength="30" disabled />\
                                                </div>\
\
                                            </div>\
                                        </div>\
\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtModelo">Modelo <label style="color:red">*</label></label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="text" name="txtModelo" id="txtModelo" required autocomplete="off" maxlength="30" disabled />\
                                                </div>\
\
                                            </div>\
                                        </div>\
\
                                        <div class="col-lg-4">\
                                            <div class="form-group">\
                                                <label for="txtObservacao">Observacao </label>\
                                                <div class="input-group">\
                                                    <div class="input-group-append">\
                                                        <div class="input-group-text" style="background-color: #FFF"><i class="fas fa-building"></i></div>\
                                                    </div>\
                                                    <input class="form-control" type="text" name="txtObservacao" id="txtObservacao" autocomplete="off" maxlength="30" disabled />\
                                                </div>\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row" id="modalArquivo">\
                                        <div class="col-lg-6">\
                                            <div class="form-group">\
                                                <label for="fuArquivoAtivo">Fotos do Ativo</label>\
                                                <div class="input-group">\
                                                    <div class="custom-file" id="customFile" lang="pt-br">\
                                                        <input type="file" class="custom-file-input" id="fuArquivoAtivo" aria-describedby="fileHelp" required disabled>\
                                                            <label class="custom-file-label" for="fuArquivoAtivo" id="labelFoto">\
                                                                Selecione a Foto\
                                                    </label>\
                                                </div>\
                                                        <div class="input-group-append">\
                                                            <button class="btn btn-success" type="button" onclick="javascript: SalvarFotosAtivo();" id="btnSalvarFotosAtivo">Adicionar</button>\
                                                        </div>\
                                                    </div>\
                                                </div>\
                                            </div>\
\
                                            <div id="divAlerta" class="alert alert-danger col-lg-6" role="alert" style="display:none"></div>\
                                        </div>\
                                        <hr />\
                                        <div class="card mb-4 shadow p-3 mb-5 bg-white rounded" style="display:none" id="modalFotos">\
                                            <div class="card-header">\
                                                <i class="fa fa-table"></i>\
                                                <span><b>Fotos Adiconadas</b></span>\
                                            </div>\
                                            <input id="txtQtd" value="0" hidden />\
                                            <div class="card-body" id="cardFotos">\
                                                <div class="row" id="imagem">\
\
                                                </div>\
                                            </div>\
                                        </div>\
\
                                        <div class="row">\
                                            <div class="col-8">\
                                                (*) - Campos Obrigatórios\
                                    </div>\
                                        </div><br />\
\
                                        <div class="d-inline">\
                                            <button type="submit" name="btnConfirmar" id="btnConfirmar" class="btn btn-success"><i class="fas fa-check"></i> Confirmar</button>\
                                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-ban"></i> Cancelar</button>\
                                        </div>\
                                    </div>\
                        </form>\
                            </div>\
                </div>\
                    </div>\
                </div>';

                if(!mostrar)
                    txt='';

    $('#novoAtivo').html(txt);

    $('.money').mask('000.000.000.000.000,00', { reverse: true });
    $('#txtCnpj').mask('00.000.000/0000-00', { reverse: true });

    CarregarTiposAtivo();
}