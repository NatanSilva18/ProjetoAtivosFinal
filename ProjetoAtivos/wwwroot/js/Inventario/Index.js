$(document).ready(function () {
    CarregarRegionais();
    CarregarTiposAtivo();
    CarregarRegionaisPesq();
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
});
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
            success: function (result) { validaLogin(result);
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
                $("#divLoading").hide(0);
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
            success: function (result) { validaLogin(result);
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        $('#cbbRegiaoPesq').append('<option value="' + result[i].codigo + '">' + result[i].descricao + '</option>');
                    }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(0);
            }
        });
    }

    $('#cbbRegiaoPesq').selectpicker('refresh');
}


function CarregarRegionais() {
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
            success: function (result) { validaLogin(result);
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
                        CarregarFiliais(cbbRegional);
                    }
                    else
                        if (regiao != 0) {
                            $("#cbbRegional").val(regiao).change();
                            $("#cbbRegional").attr('disabled', 'disabled');
                            CarregarFiliais(cbbRegional);
                        }
                        else {
                            $("#cbbRegional").removeAttr('disabled');
                        }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(0);
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
            success: function (result) { validaLogin(result);
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
                $("#divLoading").hide(0);
            }
        });
    }
    else {
        LimparCombo("cbbFilialPesq");
    }
    $('#cbbFilialPesq').selectpicker('refresh');
};


function CarregarFiliais(Combo) {
    document.getElementById("cbbFilial").required = true;

    var filial = parseInt($("#filial").val());

    var Codigo = Combo.value;
    var cbbFilial = document.getElementById("cbbFilial");
    if (cbbFilial != null) {
        document.getElementById('linhaFilial').style.display = "block ";
        document.getElementById("cbbFilial").required = true;

        $.ajax({
            type: 'POST',
            url: '/Filial/BuscarFiliais',
            async: false,
            data: { Codigo: Codigo },
            success: function (result) { validaLogin(result);
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {

                        var opt = document.createElement("option");
                        opt.value = result[i].codigo;
                        opt.text = result[i].razao;
                        cbbFilial.add(opt, cbbFilial.options[i + 1]);
                    }

                    if (filial != 0) {
                        $("#cbbFilial").val(filial).change();
                        $("#cbbFilial").attr('disabled', 'disabled');
                        BuscarSalas(cbbFilial);
                    }
                    else
                        $("#cbbFilial").removeAttr('disabled');

                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(0);
            }
        });
    }
    $('#cbbFilial').selectpicker('refresh');


};
function BuscarSalas(Combo) {

    if ($("#cbbTpAtivo").val() != 3) {
        document.getElementById("cbbSala").required = true;


        var Codigo = Combo.value;
        var cbbSala = document.getElementById("cbbSala");
        if (cbbSala != null) {
            document.getElementById('sala').style.display = "block ";
            document.getElementById("cbbSala").required = true;
            $.ajax({
                type: 'POST',
                url: '/Sala/BuscarSalas',
                async: false,
                data: { Codigo: Codigo },
                success: function (result) { validaLogin(result);
                    if (result != null && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {

                            var opt = document.createElement("option");
                            opt.value = result[i].codigo;
                            opt.text = result[i].descricao;
                            cbbSala.add(opt, cbbSala.options[i + 1]);
                        }
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(0);
                }
            });

        }
        $('#cbbSala').selectpicker('refresh');
        $('#sala').show();
    }
    else {
        $('#sala').hide();
    }
};

function LimparTabela() {
    if ($.fn.dataTable.isDataTable('#tableAtivo')) {
        $('#tableAtivo').DataTable().destroy();
    }
    $("#tbbAtivo").hide(0);
    $("#tableAtivo tr").remove();
    $("#txtDtIni").val("");
    $("#txtDtFim").val("");
    $('#cbbRegiaoPesq').selectpicker('val', '');
    $('#cbbFilialPesq').selectpicker('val', '0');
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
    $("#minhaImagemHidden").val("");
    document.getElementById("fuArquivo").value = "";
    document.getElementById("labelFoto").innerHTML = 'Selecione uma Foto';
    $("#cardRegional").show();
    $("#modalArquivo").show();

    document.getElementById("staticBackdropLabel").innerHTML = "Cadastro de Ativos";

    document.getElementById("fuArquivo").required = true;

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
                            $(td).css('border-left', '4px solid green');

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
                            $(td).css('border-left', '4px solid green');
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
function PreencherTabela(dados) {
    var i = 0;
    var ValorAtivo = 0;

    $("#tbbAtivo").show();
    var txt = '<thead>\
            <tr class="thead-light">\
                <th scope="col" width="6%">Ativo</th>\
                <th scope="col">Placa</th>\
                <th scope="col">Descrição</th>\
                <th scope="col">Valor</th>\
                <th scope="col">Estado</th>\
                <th scope="col">Filial</th>\
                <th scope="col" >Data Inventário</th>\
                <th scope="col">Obs</th>\
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

       /* if (this.ativo.status == 1) {*/
            if (this.imagem != "") {
                txt += '<tr class="galeria"><td onclick="javascript:ObterImagens(' + this.codigoAtivo + '); "><img id="minhaImagem' + i + '" src="https://imagepng.org/wp-content/uploads/2019/12/check-icone-scaled.png" class="rounded" alt="..." width=40 height=40></td><td>' + this.placa + '</td><td>' + this.descricao + '</td><td>R$' + ValorAtivo + '</td><td>' + this.estado + '</td><td>' + this.filial + '</td><td>' + this.data + '</td><td>' + this.obs + '</td><td style="display:none;">1</td><td align="right" class="form-group">'
                // txt += ' <a role="button" class="btn btn-danger" href="javascript:ExcluirLogico(' + this.codigo + ');" title="Excluir Registro"><i class="fas fa-trash"></i></a>';
                txt += ' <a role="button" class="btn btn-success"  href="javascript: BuscarLocalizacao(' + this.codigoAtivo + ');" title="Localização Ativo"><i class="fas fa-map-marker"></i></a>&nbsp;';
                
            }
            else {
                txt += '<tr class="galeria"><td></td><td>' + this.placa + '</td><td>' + this.descricao + '</td><td>R$' + ValorAtivo + '</td><td>' + this.estado + '</td><td>' + this.razao + '</td><td>' + this.data + '</td><td>' + this.obs + '</td><td style="display:none;">1</td><td align="right" class="form-group">'
                // txt += ' <a role="button" class="btn btn-danger" href="javascript:ExcluirLogico(' + this.codigo + ');" title="Excluir Registro"><i class="fas fa-trash"></i></a>';
                txt += ' <a role="button" class="btn btn-success"  href="javascript: BuscarLocalizacao(' + this.codigoAtivo + ');" title="Localização Ativo"><i class="fas fa-map-marker"></i></a>&nbsp;';
                /*
                txt += '<tr class="galeria" ondblclick="UnlockFields(); Alterar(' + this.codigo + ');"><td><img id="minhaImagem' + i + '" src="" class="rounded" alt="..." width=40 height=40></td><td>' + this.ativo.placa + '</td><td>' + this.ativo.descricao + '</td><td>R$' + ativo.valor + '</td><td>' + this.ativo.estado + '</td><td>' + this.ativo.razao + '</td><td>' + this.data + '</td><td>' + this.obs + '</td><td style="display:none;">0</td><td align="right" class="form-group">'
                txt += ' <a role="button" class="btn btn-danger" href="javascript:ExcluirLogico(' + this.codigo + ');" title="Excluir Registro"><i class="fas fa-trash"></i></a>&nbsp;';
                */
            }/*
        }
        else {
            if (this.imagem != "") {
                txt += '<tr class="galeria" ondblclick="UnlockFields(); Alterar(' + this.codigo + ');"><td onclick="javascript:ObterImagens(' + this.codigo + '); "><img id="minhaImagem' + i + '" src="' + this.imagem + '" class="rounded" alt="..." width=40 height=40></td><td>' + this.ativo.placa + '</td><td>' + this.ativo.descricao + '</td><td>R$' + ValorAtivo + '</td><td>' + this.ativo.estado + '</td><td>' + this.ativo.razao + '</td><td>' + this.data + '</td><td>' + this.obs + '</td><td style="display:none;">1</td><td align="right" class="form-group">'
                txt += ' <a role="button" class="btn btn-success"  href="javascript: BuscarLocalizacao(' + this.ativo.latitude + ',' + this.ativo.longitude + ');" title="Localização Ativo"><i class="fas fa-map-marker"></i></a>&nbsp;';
            }
            else {
                txt += '<tr class="galeria" ondblclick="UnlockFields(); Alterar(' + this.codigo + ');"><td><img id="minhaImagem' + i + '" src="" class="rounded" alt="..." width=40 height=40></td><td>' + this.ativo.placa + '</td><td>' + this.ativo.descricao + '</td><td>R$' + ValorAtivo + '</td><td>' + this.ativo.estado + '</td><td>' + this.ativo.razao + '</td><td>' + this.data + '</td><td>' + this.obs + '</td><td style="display:none;">0</td><td align="right" class="form-group">'

            }
        }*/
        txt += '</td></tr>';
        i++;
    });
    txt += '</tbody>';
    $("#tableAtivo").html(txt);
    funcaoTable("#tableAtivo");
    Rolagem();
}

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
            validaLogin(result);
            if (result != null) {
                Mapa(result.latitude, result.longitude)
            }

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(0);
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
document.querySelector('.custom-file-input').addEventListener('change', function (e) {
    var fileName = document.getElementById("fuArquivo").files[0].name;
    var nextSibling = e.target.nextElementSibling;
    nextSibling.innerText = fileName;
});

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
function MostraImagens(imgs) {

    var cont = 0;
    $.each(imgs, function () {
        cont++;

        $("#txtQtd").val(parseInt($("#txtQtd").val()) + 1);

        txr2 = this.foto;

        var imgs = $("#minhaImagemHidden").val();

        if (imgs != "")
            imgs += "**Separdor Imagem**";

        imgs += txr2;
        $("#minhaImagemHidden").val(imgs);

        var txt = '  <div class="col-lg-2" id="fotos' + cont+ '">\
                                        <div class="form-group">\
                                            <div class="card " style="width: 10rem;">\
                                                <img id="minhaImagem" src="'+ txr2 + '" class="card-img-top" alt="...">\
                                                     <p class="text-muted">Data: '+ Data(this.dataInsercao)+ '</p>\
                                                    <div class="card-body">\
                                                        <button type="button" class="btn btn-outline-danger btn-sm" onclick="javascript: ExcluirFoto('+ $("#txtQtd").val() + ')"><i class="fas fa-trash"></i></button>\
                                                    </div>\
                                                </div>\
                                            </div>\
                                        </div>';


        $("#fotos").show();
        $("#modalFotos").show();


        $("#imagem").append(txt)

    });




}

function SalvarFotos() {

    var arquivos = document.getElementById("fuArquivo");
    if (arquivos.files.length > 0) {
        $("#divLoading").show(0);

        document.getElementById('btnSalvarFotos').innerHTML = '<div class="spinner-border text-primary" role="status"><span class="sr-only" > Loading...</span></div>';
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

                        var imgs = $("#minhaImagemHidden").val();

                        if (imgs != "")
                            imgs += "**Separdor Imagem**";

                        imgs += txr2;

                        $("#minhaImagemHidden").val(imgs);

                        var txt = '  <div class="col-lg-2" id="fotos' + $("#txtQtd").val() + '">\
                                        <div class="form-group">\
                                            <div class="card " style="width: 10rem;">\
                                                <img id="minhaImagem" src="'+ txr2 + '" class="card-img-top" alt="...">\
                                                    <div class="card-body">\
                                                        <button type="button" class="btn btn-outline-danger btn-sm" onclick="javascript: ExcluirFoto('+ $("#txtQtd").val() + ')"><i class="fas fa-trash"></i></button>\
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

                    document.getElementById('btnSalvarFotos').innerHTML = 'Adicionar';
                    $("#divLoading").hide(0);

                });
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(0);
            }
        });
    }
    else {
        Mensagem("divAlerta", 'Selecione um arquivo!');
    }
    $("#divLoading").hide(0)

};
function ExcluirFoto(Codigo) {   
    var Foto = document.getElementById('fotos' + Codigo);

    if (Foto != null) {
        Foto.remove();
        $("#txtQtd").val(parseInt($("#txtQtd").val()) - 1);

        document.getElementById("fuArquivo").value = "";
        document.getElementById("labelFoto").innerHTML = 'Selecione uma Foto';

        var bases = $("#minhaImagemHidden").val().split("**Separdor Imagem**");
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

        $("#minhaImagemHidden").val(novo);
    }
    var Imagem = document.getElementById('imagem');

    if (Imagem.childElementCount == 0)
        $("#modalFotos").hide();
};
function Mensagem(div, msg) {
    $("#" + div).html(msg);
    $("#" + div).show();
    $("#" + div).delay(6000);
    $("#" + div).hide(0);
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
    document.getElementById('fuArquivo').disabled = false;
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
    document.getElementById('fuArquivo').disabled = true;
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
        success: function (result) { validaLogin(result);
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
            }
            $("#divLoading").hide(0);

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(0);
        }
    });
};

function PreencherValor(Combo) {
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
        success: function (result) { validaLogin(result);
            if (result != null) {
                $("#txtValorNota").val(result.valor);
            }
            
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(0);
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
        success: function (result) { validaLogin(result);
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

                $("#divLoading").hide(0);
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(0);
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
        success: function (result) { validaLogin(result);
            if (result.length > 0) {
                $('#galeria').modal('show');
                MontarGaleriaAtivo(result);
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(0);
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

    $("#divLoading").hide(0);
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
                        
                        
                        $("#" + dest).show();
                        document.getElementById(button).innerHTML = 'Substituir';

                    }
                    if (this.id == -1) {
                        Mensagem("divAlerta" + sulf, this.dados);
                    }

                    if (this.id == -2) {
                        Mensagem("divAlerta" + sulf, this.dados);
                    }

                    
                    $("#divLoading").hide(0);

                });
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(0);
            }
        });
    }
    else {
        Mensagem("divAlerta" + sulf, 'Selecione um arquivo!');
    }
    $("#divLoading").hide(0);

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
                $("#divLoading").hide(0);
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
                $("#divLoading").hide(0);
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
                $("#divLoading").hide(0);
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
                $("#divLoading").hide(0);
            }
        });
    }
}


function SalvarFotosInventario() {

    var arquivos = document.getElementById("fuArquivoInventario");
    if (arquivos.files.length > 0) {
        $("#divLoading").show(0);

        //document.getElementById('btnSalvarFotosInventario').innerHTML = '<div class="spinner-border text-primary" role="status"><span class="sr-only" > Loading...</span></div>';
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
                                                        <button type="button" class="btn btn-outline-danger btn-sm" onclick="javascript: ExcluirFotoInventario('+ $("#txtQtdInventario").val() + ')"><i class="fas fa-trash"></i></button>\
                                                    </div>\
                                                </div>\
                                            </div>\
                                        </div>';
                        $("#fotosInventario").show();
                        $("#modalFotosInventario").show();


                        $("#imagemInventario").append(txt)
                        //document.getElementById("imagem").innerHTML = txt;
                        $('#fuArquivoInventario').attr('disabled', 'disabled');
                    }
                    if (this.id == -1) {
                        Mensagem("divAlertaInventario", this.dados);
                    }

                    if (this.id == -2) {
                        Mensagem("divAlertaInventario", this.dados);
                    }

                    //document.getElementById('btnSalvarFotosInventario').innerHTML = 'Adicionar';
                    $("#divLoading").hide(0);

                });
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(0);
            }
        });
    }
    else {
        Mensagem("divAlerta", 'Selecione um arquivo!');
    }
    $("#divLoading").hide(0);

};

function ExcluirFotoInventario(Codigo) {
    var Foto = document.getElementById('fotosInventario' + Codigo);

    if (Foto != null) {
        Foto.remove();
        $("#txtQtdInventario").val(parseInt($("#txtQtdInventario").val()) - 1);

        document.getElementById("fuArquivoInventario").value = "";
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

    $('#fuArquivoInventario').removeAttr('disabled');
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

        var VerificaImagem = $('#minhaImagemHidden').val();
        if (VerificaImagem != "") {
            var Imagem = $('#minhaImagemHidden').val();

            $.ajax({
                type: 'POST',
                url: '/Ativo/Inventariar',
                data: {
                    Codigo: ativo, Observacao: obs, Imagem: Imagem, Latitude: Latitude, Longitude: Longitude
                },
                success: function (result) { validaLogin(result);
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
                        if (Codigo == 0) {
                            Swal.fire({
                                title: 'Sucesso',
                                type: 'success',
                                text: 'Ativo Inventariado com Sucesso',
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
                    }                   

                    $("#divLoading").hide();
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(0);
                }
            });
        }
        else {
            Mensagem("divAlertaInventario", 'Por favor Envie a Imagem');
            $("#divLoading").hide();
        }

    });
}

function ObterInventario() {
    $("#divLoading").show();
    document.getElementById('btnPesquisar').disabled = true;

    var dtIni = $("#txtDtIni").val();    
    var dtFim = $("#txtDtFim").val();    
    var regiao = parseInt($("#cbbRegiaoPesq").val());
    var filial = parseInt($("#cbbFilialPesq").val());

    var Texto = "";

    $.ajax({
        type: 'POST',
        url: '/Inventario/Buscar',
        data: { DtIni: dtIni, DtFim: dtFim, Regiao: regiao, Filial: filial },
        success: function (result) { validaLogin(result);
            if (result != null && result.length > 0) {
                PreencherTabela(result);
            }
            else {
               
                Swal.fire({
                    title: 'Oops...',
                    type: 'error',
                    type: 'icon',
                    text: 'Nenhum Item no Invetario Localizado',
                    timer: 5000
                })
               
                $("#tbbAtivo").hide(0);
                
                LimparTabela();
            }
            $("#divLoading").hide(0);
            document.getElementById('btnPesquisar').disabled = false;

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(0);
            document.getElementById('btnPesquisar').disabled = false;

        }
    });
};