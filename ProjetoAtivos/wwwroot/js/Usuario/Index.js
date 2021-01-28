
$(document).ready(function () {
    CarregarPessoas();
});
function CarregarPessoas() {
    var cbbPessoas = document.getElementById("cbbPessoas");
    if (cbbPessoas != null) {
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

                        //var opt = document.createElement("option");
                        //opt.value = result[i].codigo;
                        //opt.text = result[i].descricao;
                        //opt.setAttribute('data-tokens', result[i].descricao);
                        //cbbMotivo.add(opt, cbbMotivo.options[i + 1]);

                        $('#cbbPessoas').append('<option value="' + result[i].codigo + '">' + result[i].nome + '</option>');

                    }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }
    $('#cbbPessoas').selectpicker('refresh');
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
                        if (rowData[2] == '<span class="badge badge-success">Administrador</span>')
                            $(td).css('border-left', '4px solid green');
                        else
                            $(td).css('border-left', '4px solid blue');

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
                        if (rowData[2] == '<span class="badge badge-success">Administrador</span>')
                            $(td).css('border-left', '4px solid green');
                        else
                            $(td).css('border-left', '4px solid blue');

                    }
                }
            ]
        });
    }
};

function Cancelar() {
    $("#tbbUsuario").hide(500);
};
function LimparTabela() {
    if ($.fn.dataTable.isDataTable('#tableUsuarios')) {
        $('#tableUsuarios').DataTable().destroy();
    }
    $("#tableUsuarios tr").remove();
    $("#txtPesquisar").val("");
};

function LimparCampos() {
    $("#txtId").val(0);
    $("#txtEmail").val("");
    $("#txtSenha").val("");
    $("#txtConfSenha").val("");
    $("#txtCodigoPes").val(0);
    $("#txtNome").val("");
    $("#txtCpf").val("");
    $('#cbbPessoas').selectpicker('val', '');
    $('#cbbTipoUser').selectpicker('val', '');


    $("#divPessoa").show();
    $("#btnPesquisarPessoa").show();
};
function Nivel(Nivel) {
    if (Nivel == 1)
        return '<span class="badge badge-success">Administrador</span>';
    else
        return '<span class="badge badge-primary">Comum</span>';
};
function PreencherTabela(dados) {
    var txt = '<thead>\
            <tr class="thead-light">\
                <th scope="col">#ID</th>\
                <th scope="col">Email</th>\
                <th scope="col">Nivel</th>\
                <th scope="col">Nome Pessoa</th>\
                <th scope="col"></th>\
            </tr>\
        </thead >\
        <tbody>';
    $.each(dados, function () {
        txt += '<tr ondblclick="Alterar(' + this.codigo + ');"><th scope="row">' + this.codigo + '</th><td>' + this.email + '</td><td>' + Nivel(this.nivel) + '</td><td>' + this.pesNome + '</td><td align="right" class="form-group">'
        txt += '<a role="button" class="btn btn-warning" href="javascript:Alterar(' + this.codigo + ');" title="Editar Registro"><i class="fas fa-edit"></i></a>'
        txt += ' <a role="button" class="btn btn-danger" href="javascript:Excluir(' + this.codigo + ');" title="Excluir Registro"><i class="fas fa-trash"></i></a>';
        txt += '</td></tr>';

    });
    txt += '</tbody>';
    $("#tableUsuarios").html(txt);
    funcaoTable("#tableUsuarios");
};

function Alterar(Codigo) {

    $.ajax({
        type: 'POST',
        url: '/Usuario/BuscarUsuario',
        data: {
            Codigo: Codigo
        },
        success: function (result) {

            if (result != null) {
                $('#novoUsu').modal('show');

                $("#txtId").val(result.codigo);
                $("#txtEmail").val(result.email);
                $("#txtSenha").val(result.senha);
                $("#txtConfSenha").val(result.senha);

                $('#cbbTipoUser').selectpicker('val', result.nivel);

                $('#cbbPessoas').selectpicker('val', result.codigoPessoa);
            }

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
};
function Excluir(Codigo) {
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
                url: '/Usuario/Excluir',
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
                        })
                        Pesquisar();
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
                                Pesquisar();
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


function Pesquisar() {
    $("#divLoading").show(300);
    $("#tbbUsuario").show(300);

    var Chave = $("#txtPesquisar").val();
    var Filtro = document.querySelector('input[name="rdFiltro"]:checked').value;

    $.ajax({
        type: 'POST',
        url: '/Usuario/ObterUsuarios',
        data: { Chave: Chave, Filtro: Filtro },
        success: function (result) {
            if (result != null && result.length > 0) {
                PreencherTabela(result);
            }
            else {
                Swal.fire({
                    title: 'Oops...',
                    text: 'Nenhum Usuario Encontrado',
                    type: 'error',
                    timer: 5000
                })
                LimparTabela();
                $("#tbbUsuario").hide(0);
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
    var Codigo = $("#txtId").val();
    var Login = $("#txtEmail").val();
    var Senha = $("#txtSenha").val();
    var TipoUsuario = $("#cbbTipoUser").val();
    var CodigoPessoa = $("#cbbPessoas").val();
    var ConfSenha = document.getElementById('txtConfSenha');
    if (Senha == ConfSenha.value) {
        $.ajax({
            type: 'POST',
            url: '/Usuario/Gravar',
            data: {
                Codigo: Codigo, Login: Login, Senha: Senha, TipoUsuario: TipoUsuario, CodigoPessoa: CodigoPessoa
            },
            success: function (result) {
                $('#novoUsu').modal('hide');

                if (result.length > 0) {
                    Swal.fire({
                        title: 'Oops...',
                        type: 'error',
                        text: result,
                    })
                }
                else {
                    if (Codigo > 0) {
                        Swal.fire({
                            title: 'Sucesso',
                            type: 'success',
                            text: 'Alterado com Sucesso',
                            timer: 5000
                        })
                    }
                    else {
                        Swal.fire({
                            title: 'Sucesso',
                            type: 'success',
                            text: 'Gravado com Sucesso',
                            timer: 5000
                        })
                    }
                }
                Pesquisar();
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(400);
            }
        });
    }
    else {
        ConfSenha.setCustomValidity("Senhas Não Conferem!");
    }
       
};
function ValidarSenha() {
    var Senha = $("#txtSenha").val();
    var ConfSenha = document.getElementById('txtConfSenha');

    if (Senha != ConfSenha.value)
        ConfSenha.setCustomValidity("Senhas Não Conferem!");
    else
        ConfSenha.setCustomValidity('');

};
function ExibirSenha(Campo, NameIcon) {
    var element = document.getElementById('' + NameIcon);

    var key_attr = $('#' + Campo).attr('type');

    if (key_attr != 'text') {

        $('#' + Campo).attr('type', 'text');
        element.classList.remove('fa-eye');
        element.classList.add('fa-eye-slash');

    } else {

        $('#' + Campo).attr('type', 'password');
        element.classList.remove('fa-eye-slash');
        element.classList.add('fa-eye');
    }

};
