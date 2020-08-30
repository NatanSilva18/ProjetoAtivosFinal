$(document).ready(function () {
    
});
function LimparTabela() {
    if ($.fn.dataTable.isDataTable('#tableTipoAtivo')) {
        $('#tableTipoAtivo').DataTable().destroy();
    }
    $("#tbbTipoAtivo").hide(0);
    $("#tableTipoAtivo tr").remove();
    $("#txtPesquisar").val("");
    var Radio = document.getElementsByName("rdAtivo"); Radio[0].checked = true;
};
function LimparCampos() {
    $("#txtId").val(0);
    $("#txtOperacao").val(0);
    $("#txtDescricao").val("");

    document.getElementById("staticBackdropLabel").innerHTML = "Cadastro de Tipos de Ativo";

};
function Rolagem() {
    var $anchor = $(this);
    $('html, body').stop().animate({
        scrollTop: ($($anchor.attr('tbbTipoAtivo')).offset().top)
    }, 1000, 'easeInOutExpo');
};

function GerarRelatorio() {
    var doc = new jsPDF()
    doc.autoTable({
        html: '#tableTipoAtivo'
    })
    doc.save('TipoAtivo.pdf');

};
function GerarExcel() {
    var htmls = "";
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
    var base64 = function (s) {
        return window.btoa(unescape(encodeURIComponent(s)))
    };

    var format = function (s, c) {
        return s.replace(/{(\w+)}/g, function (m, p) {
            return c[p];
        })
    };

    htmls = document.getElementById('tableTipoAtivo').innerHTML;

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
function Status(Ativo) {
    if (Ativo)
        return '<span class="badge badge-success">Ativo</span>';
    else
        return '<span class="badge badge-danger">Inativo</span>';
};
function PreencherTabela(dados) {
    $("#tbbTipoAtivo").show(300);
    var txt = '<thead>\
            <tr class="thead-light">\
                <th scope="col">#ID</th>\
                <th scope="col">Descricao</th>\
                <th scope="col">Valor</th>\
                <th scope="col">Status</th>\
                <th scope="col" width="10%"></th>\
            </tr>\
        </thead >\
        <tbody>';
    $.each(dados, function () {

        if (this.stAtivo == 1) {
            txt += '<tr ondblclick="Alterar(' + this.codigo + ');"><th scope="row">' + this.codigo + '</th><td>' + this.descricao + '</td><td>R$ ' + this.valor.toFixed(2) +'</td><td>'  + Status(this.stAtivo) + '</td><td align="right" class="form-group">'
            txt += '<a role="button" class="btn btn-warning" href="javascript:Alterar(' + this.codigo + ');" title="Editar TipoAtivo"><i class="fas fa-edit"></i></a>'
            txt += ' <a role="button" class="btn btn-danger" href="javascript:ExcluirLogico(' + this.codigo + ');" title="Excluir TipoAtivo"><i class="fas fa-trash"></i></a>';

        }
        else {
            txt += '<tr ondblclick="Ativar(' + this.codigo + ');"><td>' + this.codigo + '</td><td>' + this.descricao + '</td><td>R$ ' + this.valor.toFixed(2) +'</td><td>' + Status(this.stAtivo) + '</td><td align="right" class="form-group">'
            txt += '<a role="button" class="btn btn-success" href="javascript:Ativar(' + this.codigo + ');" title="Ativar TipoAtivo"><i class="fas fa-check"></i></a>';
        }
        txt += '</td></tr>';
    });
    txt += '</tbody>';
    $("#tableTipoAtivo").html(txt);
    funcaoTable("#tableTipoAtivo");
    Rolagem();
};

function ObterTiposAtivos() {
    $("#divLoading").show(300);
    var Chave = $("#txtPesquisar").val();
    var Filtro = document.querySelector('input[name="rdFiltro"]:checked').value;
    var Ativo = document.querySelector('input[name="rdAtivo"]:checked').value;
    var Radio = document.getElementsByName("rdAtivo");
    var Texto = "";

    $.ajax({
        type: 'POST',
        url: '/TipoAtivo/ObterTiposAtivos',
        data: { Chave: Chave, Filtro: Filtro, Ativo: Ativo },
        success: function (result) {
            if (result != null && result.length > 0) {
                PreencherTabela(result);
            }
            else {
                if (Ativo == "1") {
                    Texto = "Nenhuma TipoAtivo Encontrado!";
                }
                else {
                    Texto = "Nenhum TipoAtivo INATIVO Encontrado!";

                }
                Swal.fire({
                    title: 'Oops...',
                    text: Texto,
                    type: 'error',
                    timer: 5000
                })
                $("#tbbTipoAtivo").hide(0);
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
    var Descricao = $("#txtDescricao").val();
    var Valor = $("#txtValor").val();
    var StAtivo = $('#cbAtivo').is(':checked');
    var Operacao = $("#txtOperacao").val();

    $.ajax({
        type: 'POST',
        url: '/TipoAtivo/Gravar',
        data: {
            Codigo: Codigo, Descricao: Descricao, Valor: Valor, StAtivo: StAtivo, Operacao: Operacao
        },
        success: function (result) {
            $('#novaTipoAtivo').modal('hide');

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
            ObterTiposAtivos();
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
                url: '/TipoAtivo/ExcluirLogico',
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
                        ObterTiposAtivos();
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
                                ObterTiposAtivos();
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

    document.getElementById("staticBackdropLabel").innerHTML = "Alteração de TiposAtivo";

    $.ajax({
        type: 'POST',
        url: '/TipoAtivo/BuscarTipoAtivo',
        data: {
            Codigo: Codigo
        },
        success: function (result) {

            if (result != null) {

                $('#novaTipoAtivo').modal('show');
                
                $("#txtId").val(result.codigo);
                $("#txtOperacao").val(1);
                $("#txtDescricao").val(result.descricao);
                $("#txtValor").val(result.valor);

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
        url: '/TipoAtivo/Ativar',
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
                ObterTiposAtivos();
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
                        ObterTiposAtivos();
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
