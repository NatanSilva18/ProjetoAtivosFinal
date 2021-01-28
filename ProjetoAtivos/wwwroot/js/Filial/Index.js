$(document).ready(function () {
    CarregarRegionais();
    CarregarPessoas();
});
function CarregarRegionais() {
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

function CarregarPessoas() {
    var cbbGerente = document.getElementById("cbbGerente");
    if (cbbGerente != null) {
        var Chave = "";
        var Filtro = "Nome";
        var Ativo = 1;

        $.ajax({
            type: 'POST',
            url: '/Pessoa/ObterPessoas',
            async: false,
            data: { Chave: Chave, Filtro: Filtro, Ativo: Ativo },
            success: function (result) {
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {

                        var opt = document.createElement("option");
                        opt.value = result[i].codigo;
                        opt.text = result[i].nome + ' - '+result[i].cargo;
                        cbbGerente.add(opt, cbbGerente.options[i + 1]);
                    }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }
    $('#cbbGerente').selectpicker('refresh');

};

function LimparTabela() {
    if ($.fn.dataTable.isDataTable('#tableFilial')) {
        $('#tableFilial').DataTable().destroy();
    }
    $("#tbbFilial").hide(0);
    $("#tableFilial tr").remove();
    $("#txtPesquisar").val("");
    var Radio = document.getElementsByName("rdAtivo"); Radio[0].checked = true;
};

function LimparCampos() {
    $("#txtId").val(0);
    $("#txtOperacao").val(0);
    $("#cbbRegional").val("");
    $("#cbbGerente").val("");
    $("#txtFilial").val("");
    $("#txtCnpj").val("");
    $("#txtLogradouro").val("");
    $("#txtNumero").val("");
    $("#txtBairro").val("");
    $("#txtCidade").val("");
    $("#txtCep").val("");
    $("#cbbEstado").val("");
    $("#txtEndereco").val(0);
    document.getElementById("staticBackdropLabel").innerHTML = "Cadastro de Filiais";

};
function Rolagem() {
    var $anchor = $(this);
    $('html, body').stop().animate({
        scrollTop: ($($anchor.attr('tbbFilial')).offset().top)
    }, 1000, 'easeInOutExpo');
};
function GerarRelatorio() {
    var doc = new jsPDF()
    doc.autoTable({
        html: '#tableFilial'
    })
    doc.save('Filiais.pdf');

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

    htmls = document.getElementById('tableFilial').innerHTML;

    var ctx = {
        worksheet: 'Worksheet',
        table: htmls
    }


    var link = document.createElement("a");
    link.download = "export.xls";
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
            "bFilter": false,
            columnDefs: [
                {
                    targets: 0,
                    createdCell: function (td, cellData, rowData, row, col) {
                        $(td).css('border-radius', '4px');
                        if (rowData[5] == '<span class="badge badge-danger">Inativo</span>')
                            $(td).css('border-left', '4px solid red');
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
            "bFilter": false,
            columnDefs: [
                {
                    targets: 0,
                    createdCell: function (td, cellData, rowData, row, col) {
                        $(td).css('border-radius', '4px');
                        if (rowData[5] == '<span class="badge badge-danger">Inativo</span>')
                            $(td).css('border-left', '4px solid red');
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
function StatusRegional(Ativo, Descricao) {     //mmexer no controller e dao .. 
    if (Ativo)
        return Descricao;
    else
        return '<span class="badge badge-danger">Reg. Inativo</span>';
};
function PreencherTabela(dados) {
    $("#tbbFilial").show(300);
    var txt = '<thead>\
            <tr class="thead-light">\
                <th scope="col">#ID</th>\
                <th scope="col">Regional</th>\
                <th scope="col">Razão</th>\
                <th scope="col">Estado</th>\
                <th scope="col">Responsavel</th>\
                <th scope="col">Status</th>\
                <th scope="col" width="10%"></th>\
            </tr>\
        </thead >\
        <tbody>';
    $.each(dados, function () {

        if (this.stAtivo == 1) {
            txt += '<tr ondblclick="Alterar(' + this.codigo + ');"><th scope="row">' + this.codigo + '</th><td>' + StatusRegional(this.regAtivo,this.regional) + '</td><td>' + this.razao + '</td><td>'+this.estado+'</td><td>'+this.responsavel+'</td><td>' + Status(this.stAtivo) + '</td><td align="right" class="form-group">'
            txt += '<a role="button" class="btn btn-warning" href="javascript:Alterar(' + this.codigo + ');" title="Editar Registro"><i class="fas fa-edit"></i></a>'
            txt += ' <a role="button" class="btn btn-danger" href="javascript:ExcluirLogico(' + this.codigo + ');" title="Excluir Registro"><i class="fas fa-trash"></i></a>';

        }
        else {
            txt += '<tr ondblclick="Ativar(' + this.codigo + ');"><td>' + this.codigo + '</td><td>' + this.regional + '</td><td>' + this.razao + '</td><td>' + this.estado +'</td><td>' + this.responsavel +'</td><td>' + Status(this.stAtivo) + '</td><td align="right" class="form-group">'
            txt += '<a role="button" class="btn btn-success" href="javascript:Ativar(' + this.codigo + ');" title="Ativar Registro"><i class="fas fa-check"></i></a>';
        }
        txt += '</td></tr>';
    });
    txt += '</tbody>';
    $("#tableFilial").html(txt);
    funcaoTable("#tableFilial");
    Rolagem();
};

function ObterFiliais() {
    $("#divLoading").show(300);
    var Chave = $("#txtPesquisar").val();
    var Filtro = document.querySelector('input[name="rdFiltro"]:checked').value;
    var Ativo = document.querySelector('input[name="rdAtivo"]:checked').value;
    var Radio = document.getElementsByName("rdAtivo");
    var Texto = "";

    $.ajax({
        type: 'POST',
        url: '/Filial/ObterFiliais',
        data: { Chave: Chave, Filtro: Filtro, Ativo: Ativo },
        success: function (result) {
            if (result != null && result.length > 0) {
                PreencherTabela(result);
            }
            else {
                if (Ativo == "1") {
                    Texto = "Nenhuma Filial Encontrada!";
                }
                else {
                    Texto = "Nenhuma Filial INATIVA Encontrada!";

                }
                Swal.fire({
                    title: 'Oops...',
                    text: Texto,
                    type: 'error',
                    timer: 5000
                })
                $("#tbbRegional").hide(0);
                Radio[0].checked = true;
                LimparTabela();
            }
            $("#divLoading").hide(300);
            $("#txtPesquisar").val("");

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(300);
        }
    });
};

function Gravar() {
    $("#divLoading").show(400);
    var Codigo = $("#txtId").val();
    var Regional = $("#cbbRegional").val();
    var Responsavel = $("#cbbGerente").val();
    var Razao = $("#txtFilial").val();
    var Cnpj = $("#txtCnpj").val();
    var EndReferencia = "Teste";
    var EndLogradouro = $("#txtLogradouro").val();
    var EndNumero = $("#txtNumero").val();
    var EndCep = $("#txtCep").val();
    var EndBairro = $("#txtBairro").val();
    var EndCidade = $("#txtCidade").val();
    var EndEstado = $("#cbbEstado").val();
    var Endereco = $("#txtEndereco").val();


    var StAtivo = $('#cbAtivo').is(':checked');
    var Operacao = $("#txtOperacao").val();

    $.ajax({
        type: 'POST',
        url: '/Filial/Gravar',
        data: {
            Codigo: Codigo, Razao: Razao, Cnpj: Cnpj, StAtivo: StAtivo, Endereco: Endereco, EndReferencia: EndReferencia, EndLogradouro: EndLogradouro, EndNumero: EndNumero, EndCep: EndCep, EndBairro: EndBairro,
            EndCidade: EndCidade, EndEstado: EndEstado, Responsavel: Responsavel, Regional: Regional, Operacao:Operacao
        },
        success: function (result) {
            $('#novaFilial').modal('hide');

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
                        text: 'Registro Gravado com Sucesso',
                        timer: 5000
                    })
                }
                else {
                    Swal.fire({
                        title: 'Sucesso',
                        type: 'success',
                        text: 'Registro Alterado com Sucesso',
                        timer: 5000
                    })
                }

                $("#divLoading").hide(400);
            }
            ObterFiliais();
            LimparCampos();
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
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
                url: '/Filial/ExcluirLogico',
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
                        ObterFiliais();
                    }
                    else {
                        Swal.fire({
                            title: 'Sucesso',
                            text: "Registro Excluido com Sucesso!",
                            type: 'success',
                            allowEscapeKey: false,
                            allowOutsideClick: false,
                            confirmButtonColor: '#3085d6',
                        }).then((result) => {
                            if (result.value) {
                                ObterFiliais();
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
function Alterar(Codigo) {

    document.getElementById("staticBackdropLabel").innerHTML = "Alteração de Filiais";

    $.ajax({
        type: 'POST',
        url: '/Filial/BuscarFilial',
        data: {
            Codigo: Codigo
        },
        success: function (result) {

            if (result != null) {

                $('#novaFilial').modal('show');

                $("#txtId").val(result.codigo);
                $("#txtOperacao").val(1);
                $('#cbbRegional').selectpicker('val', result.regCodigo);
                $('#cbbGerente').selectpicker('val', result.respCodigo);
                $("#txtFilial").val(result.razao);
                $("#txtCnpj").val(result.cnpj);
                $("#txtLogradouro").val(result.logradouro);
                $("#txtNumero").val(result.numero);
                $("#txtBairro").val(result.bairro);
                $("#txtCep").val(result.cep);
                $("#txtCidade").val(result.cidade);
                $('#cbbEstado').selectpicker('val', result.estado);
                $("#txtEndereco").val(result.endereco);
            }

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
};
function Ativar(Codigo) {

    $.ajax({
        type: 'POST',
        url: '/Filial/Ativar',
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
                ObterFiliais();
            }
            else {
                var Radio = document.getElementsByName("rdAtivo"); Radio[0].checked = true;
                Swal.fire({
                    title: 'Sucesso',
                    text: "Registro Ativo com Sucesso",
                    type: 'success',
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    confirmButtonColor: '#3085d6'
                }).then((result) => {
                    if (result.value) {
                        ObterFiliais();
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

