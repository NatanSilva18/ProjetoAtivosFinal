$(document).ready(function () {
    CarregarFiliais();
    CarregarMotivos();
});

function CarregarFiliais() {
    var filial = $("#filial").val();
    var cbbFilialOrigem = document.getElementById("cbbFilialOrigem");
    var cbbFilialDestino = document.getElementById("cbbFilialDestino");
    if (cbbFilialOrigem != null && cbbFilialDestino != null) {
        var Chave = "";
        var Filtro = "Razao";
        var Ativo = 1;

        $.ajax({
            type: 'POST',
            url: '/Filial/ObterFiliais',
            async: false,
            data: { Chave: Chave, Filtro: Filtro, Ativo: Ativo },
            success: function (result) {
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {

                        $('#cbbFilialOrigem').append('<option value="' + result[i].codigo + '">' + result[i].razao + '</option>');
                        $('#cbbFilialOrigemPesquisa').append('<option value="' + result[i].codigo + '">' + result[i].razao + '</option>');
                        $('#cbbFilialDestinoPesquisa').append('<option value="' + result[i].codigo + '">' + result[i].razao + '</option>');

                        if (filial != result[i].codigo)
                            $('#cbbFilialDestino').append('<option value="' + result[i].codigo + '">' + result[i].razao + '</option>');

                    }

                    if (filial != 0) {
                        $('#cbbFilialOrigem').val(filial).change();
                        $('#cbbFilialOrigem').attr('disabled', 'disabled');
                    }
                    else
                        $("#cbbFilialOrigem").removeAttr('disabled');
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }
    $('#cbbFilialOrigem').selectpicker('refresh');
    $('#cbbFilialDestino').selectpicker('refresh');
    $('#cbbFilialOrigemPesquisa').selectpicker('refresh');
    $('#cbbFilialDestinoPesquisa').selectpicker('refresh');

};

function CarregarMotivos() {
    var cbbMotivo = document.getElementById("cbbMotivo");
    if (cbbMotivo != null) {
        var Chave = "";
        var Filtro = "Descricao";
        var Ativo = 1;

        $.ajax({
            type: 'POST',
            url: '/Motivo/ObterMotivos',
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

                        $('#cbbMotivo').append('<option value="' + result[i].codigo + '">' + result[i].descricao + '</option>');

                    }
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }
    $('#cbbMotivo').selectpicker('refresh');
};


function BuscarLocaisDestino(Combo) {
    var Codigo = Combo.value;
    if (Codigo > 0) {
        var cbbLocalDestino = document.getElementById("cbbLocalDestino");
        if (cbbLocalDestino != null) {

            $.ajax({
                type: 'POST',
                url: '/Sala/BuscarSalas',
                async: false,
                data: { Codigo: Codigo },
                success: function (result) {
                    if (result != null && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {

                            /*var opt = document.createElement("option");
                            opt.value = result[i].codigo;
                            opt.text = result[i].descricao;
                            cbbLocalDestino.add(opt, cbbLocalDestino.options[i + 1]);*/
                            $('#cbbLocalDestino').append('<option value="' + result[i].codigo + '">' + result[i].descricao + '</option>');

                        }
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(300);
                }
            });

            ObterResponsavel(Codigo, 2);
        }

    }
    else {
        $("#txtResponsavelDestino").val("");
    }
    $('#cbbLocalDestino').selectpicker('refresh');
};

function ObterResponsavel(Codigo, Contr) {
    if (Codigo > 0) {
        $.ajax({
            type: 'POST',
            url: '/Filial/BuscarFilial',
            async: false,
            data: { Codigo: Codigo },
            success: function (result2) {
                if (result2 != null) {
                    if (Contr == 1)
                        $("#txtResponsavelOrigem").val(result2.responsavel);
                    else
                        $("#txtResponsavelDestino").val(result2.responsavel);
                }

            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
    }
    else {
        if (Contr == 1)
            $("#txtResponsavelOrigem").val("");
        else
            $("#txtResponsavelDestino").val("");
    }
};

function BuscarLocais(Combo) {
    var Codigo = Combo.value;
    if (Codigo > 0) {
        var cbbLocalOrigem = document.getElementById("cbbLocalOrigem");
        if (cbbLocalOrigem != null) {

            $.ajax({
                type: 'POST',
                url: '/Sala/BuscarSalas',
                async: false,
                data: { Codigo: Codigo },
                success: function (result) {
                    if (result != null && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {

                            /*var opt = document.createElement("option");
                            opt.value = result[i].codigo;
                            opt.text = result[i].descricao;
                            cbbLocalOrigem.add(opt, cbbLocalOrigem.options[i + 1]);*/
                            $('#cbbLocalOrigem').append('<option value="' + result[i].codigo + '">' + result[i].descricao + '</option>');

                        }

                    }

                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(300);
                }
            });
            ObterResponsavel(Codigo, 1);
        }

    }
    else {
        $("#txtResponsavelOrigem").val("");

    }
    $('#cbbLocalOrigem').selectpicker('refresh');

};
function MostrandoDE(Sala, Filial) {
    document.getElementById('mostrandoDE').innerHTML = '</br> <label>Mostrando Ativos de: <b>' + Filial + '</b>';
};
function BuscarAtivos(Combo) {
    var Local = Combo.value;
    var cbbAtivos = document.getElementById("cbbAtivos");
    var cbbFilialOrigem = document.getElementById("cbbFilialOrigem");
    if (cbbFilialOrigem != null) {

        if (Local > 0) {
            $.ajax({
                type: 'POST',
                url: '/Ativo/BuscarAtivos',
                async: false,
                data: { Local: Local },
                success: function (result) {
                    if (result != null && result.length > 0) {

                        for (var i = 0; i < result.length; i++) {

                            var opt = document.createElement("option");
                            opt.value = result[i].codigo;
                            opt.text = result[i].placa+'-'+ result[i].descricao;
                            cbbAtivos.add(opt, cbbAtivos.options[i + 1]);
                            //$('#cbbAtivos').append('<option value="' + result[i].codigo + '">' + result[i].descricao + '</option>');

                        }
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(300);
                }
            });
            var Sala = $('#cbbLocalOrigem').find(":selected").text();
            var Filial = $('#cbbFilialOrigem').find(":selected").text();
            MostrandoDE(Sala, Filial);
            $('#cbbAtivos').selectpicker('refresh');

        }
    }
};

function LimparCombo(Name) {
    var select = document.getElementById(Name);
    if (select != null) {
        var length = select.options.length;
        if (length > 1) {
            for (var i = length - 1; i >= 1; i--) {
                select.remove(i);
            }
        }
    }

};
function LimparTabelaItens() {
    $("#tbbItensTransferencia").hide(0);
    $("#tableItensTransferencia tr").remove();
};

function LimparTabela() {
    if ($.fn.dataTable.isDataTable('#tableTransferencia')) {
        $('#tableTransferencia').DataTable().destroy();
    }
    $("#tbbTransferencia").hide(0);
    $("#tableTransferencia tr").remove();
    $("#txtPesquisar").val("");
    var Radio = document.getElementsByName("rdAtivo"); Radio[0].checked = true;
    $('#cbbFilialOrigemPesquisa').selectpicker('val', '');
    $('#cbbFilialDestinoPesquisa').selectpicker('val', '');
};

function LimparReceber() {
    $("#tableItensTransferenciaRec tr").remove();
    $("#tbbItensTransferenciaRec").hide();

    $("#txtObsRec").val("");
    $("#qtdAtivosRec").val("0");
    LimparCombo();
    LimparCombo();
    document.getElementById("fuArquivoRec").value = "";
    document.getElementById("labelFoto").innerHTML = 'Selecione uma Foto';
}
function LimparTransferencia() {
    $('#cbbMotivo').selectpicker('val', '');
    $("#txtDescricao").val("");
    $('#cbbFilialOrigem').selectpicker('val', '');
    $("#txtResponsavelOrigem").val("");
    $('#cbbFilialDestino').selectpicker('val', '');
    $("#txtResponsavelDestino").val("");

    //itens transferencia ativos
    $("#tableItensTransferencia tr").remove();
    $("#tbbItensTransferencia").hide(300);

    document.getElementById('Doc').innerHTML = '';
    document.getElementById('confirmarCampos').innerHTML = '';
    document.getElementById('confirmarDocs').innerHTML = '';

    //itens confirmar transferencia ativos
    $("#tableConfirmarItens tr").remove();
    $("#tbbConfirmarItens").hide(300);
    $("#modalDocs").hide(300);
    $("#tbbConfirmarDocs").hide(300);


    //desabilita todos campos menos o default
    document.getElementById('ativos').classList.add('disabled');
    document.getElementById('documentos').classList.add('disabled');
    document.getElementById('confirmar').classList.add('disabled');

    //remove os itens atiivos menos o default
    document.getElementById('origemDestino').classList.add('active');
    document.getElementById('ativos').classList.remove('active');
    document.getElementById('documentos').classList.remove('active');
    document.getElementById('confirmar').classList.remove('active');

    $('#nav-OrigemDestino').tab('show');
    document.getElementById('nav-ativos').classList.remove('show');
    document.getElementById('nav-documentos').classList.remove('show');
    document.getElementById('nav-confirmar').classList.remove('show');

    document.getElementById('nav-ativos').classList.remove('active');
    document.getElementById('nav-documentos').classList.remove('active');
    document.getElementById('nav-confirmar').classList.remove('active');
};
function Status(Ativo) {
    if (Ativo)
        return '<span class="badge badge-success">Ativo</span>';
    else
        return '<span class="badge badge-danger">Inativo</span>';
};
function MontarTableConfirmar(result, Imagem, aprov = false) {
    if (aprov)
        Tabela = document.getElementById("tableConfirmarItensAprov");
    else
        Tabela = document.getElementById("tableConfirmarItens");

    var Linhas = Tabela.getElementsByTagName('tr');
    var txt = '';
    if (Linhas.length == 0) {
        txt = '<thead>\
                             <tr class="thead-light">\
                                <th scope="col">Estado Atual</th>\
                                <th scope="col">Placa</th>\
                                <th scope="col">Descrição</th>\
                                <th scope="col">Estado</th>\
                                <th scope="col">Status</th>\
                                <th scope="col" width="15%"></th>\
                            </tr>\
                                </thead >\
                                <tbody>';
        txt += '<tr   id=prov' + result.codigo + ' class="galeria"><td ><img src="' + Imagem + '" class="rounded float-left" alt="' + result.codigo + '" width=40 height=40></td><td>' + result.placa + '</td><td>' + result.descricao + '</td><td>' + result.estado + '</td><td>' + Status(result.stAtivo) + '</td><td align="right" class="form-group">'

        txt += '</td ></tr></tbody>';
    }
    else {
        txt += '<tr id=prov' + result.codigo + '  class="galeria"><td ><img  src="' + Imagem + '" class="rounded float-left" alt="' + result.codigo + '" width=40 height=40></td><td>' + result.placa + '</td><td>' + result.descricao + '</td><td>' + result.estado + '</td><td>' + Status(result.stAtivo) + '</td><td align="right" class="form-group">'

        txt += '</td ></tr>';
    }

    return txt;
}

function AdicionarAtivos(rec = '') {

    var Imagem = SalvarFotos(rec);

    if (Imagem != "") {
        var Codigo = $('#cbbAtivos' + rec).val();
        $('#cbbAtivos' + rec).val("");
        var Tabela = document.getElementById("tableItensTransferencia" + rec);
        var Linhas = Tabela.getElementsByTagName('tr');

        $("#tbbItensTransferencia" + rec).show(300)
        $("#divLoading").show();
        $.ajax({
            type: 'POST',
            url: '/Ativo/BuscarAtivo',
            data: { Codigo: Codigo },
            success: function (result) {
                if (Linhas.length == 0) {   //insere o cabeçalho ... 
                    var txt = '<thead>\
                             <tr class="thead-light">\
                                <th scope="col">Estado Atual</th>\
                                <th scope="col">Placa</th>\
                                <th scope="col">Descrição</th>\
                                <th scope="col">Estado</th>\
                                <th scope="col">Status</th>';
                    if (rec != '')
                        txt += '<th scope="col">Sala Destino</th>';

                    txt += '<th scope="col" width="15%"></th>\
                            </tr>\
                                </thead >\
                                <tbody>';
                    txt += '<tr class="galeria" id=' + result.codigo + ' ondblclick="Alterar(' + result.codigo + ');"><td ><img id="minhaImagem' + rec + Linhas.length + '" src="' + Imagem + '" class="rounded float-left" alt="' + result.codigo + '" width=40 height=40></td><td>' + result.placa + '</td><td>' + result.descricao + '</td><td>' + result.estado + '</td><td>' + Status(result.stAtivo) + '</td>';
                    if (rec != '') {
                        var select = document.querySelector('cbbSalaDestino');
                        //var option = select.children[select.selectedIndex];
                        var sala = $("#cbbSalaDestino option:selected").html();

                        txt += '<td><input type="hidden" id="salaDestino' + Linhas.length + '" value="' + $("#cbbSalaDestino").val() + '"/>' + sala + '</td>';
                    }

                    txt += ' <td align="right" class="form-group"><a role="button" class="btn btn-danger" href="javascript:RemoverItem(' + result.codigo + ',\'' + rec + '\',\'' + result.descricao + '\');" title="Excluir Registro"><i class="fas fa-trash"></i></a>';

                    txt += '</td ></tr></tbody>';

                    $("#tableItensTransferencia" + rec).html(txt);
                    $("#tableConfirmarItens" + rec).html(MontarTableConfirmar(result, Imagem));
                    $("#divLoading").hide(300);

                }
                else {
                    var Validar = document.getElementById("" + result.codigo);
                    var Qtd = Linhas.length;
                    if (Validar == null) {  //se ja tem na lista n faz nada so adiciona
                        txt += '<tr class="galeria" id=' + result.codigo + ' ondblclick="Alterar(' + result.codigo + ');"><td ><img id="minhaImagem' + rec + (Linhas.length - 1) + '" src="' + Imagem + '" class="rounded float-left" alt="' + result.codigo + '" width=40 height=40></td><td>' + result.placa + '</td><td>' + result.descricao + '</td><td>' + result.estado + '</td><td>' + Status(result.stAtivo) + '</td>';
                        if (rec != '') {
                            var select = document.querySelector('cbbSalaDestino');
                            //var option = select.children[select.selectedIndex];
                            var sala = $("#cbbSalaDestino option:selected").html();

                            txt += '<td><input type="hidden" id="salaDestino' + (Linhas.length - 1) + '" value="' + $("#cbbSalaDestino").val() + '"/>' + sala + '</td>';
                        }

                        txt += ' <td align="right" class="form-group"><a role="button" class="btn btn-danger" href="javascript:RemoverItem(' + result.codigo + ',\'' + rec + '\',\'' + result.descricao + '\');" title="Excluir Registro"><i class="fas fa-trash"></i></a>';

                        txt += '</td ></tr>';
                        $("#tableConfirmarItens" + rec + " tbody").append(MontarTableConfirmar(result, Imagem));

                    }

                    $("#tableItensTransferencia" + rec + " tbody").append(txt);
                    $("#divLoading").hide(300);

                }

                if (rec == '') {
                    SegundaEtapa();
                    document.getElementById('btnSalvarFotos').innerHTML = 'Adicionar';
                    $("#divLoading").hide(300);

                }
                else {
                    $("#cbbAtivosRec option[value='" + result.codigo + "']").remove();
                    $('#cbbAtivosRec').selectpicker('refresh');
                    VerificaGravaRec();
                    document.getElementById('btnSalvarFotosReceber').innerHTML = 'Adicionar';
                    $("#divLoading").hide(300);
                }

            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                $("#divLoading").hide(300);
            }
        });
        document.getElementById("fuArquivo" + rec).value = "";
        document.getElementById("labelFoto" + rec).innerHTML = 'Selecione uma Foto';
        $('#cbbAtivos' + rec).selectpicker('val', '');
    }
    else {
        if (rec == 'Rec')
            document.getElementById('btnSalvarFotosReceber').innerHTML = 'Adicionar';
        else {
            document.getElementById('btnSalvarFotos').innerHTML = 'Adicionar';
        }
    }
};

function ordenarSelect(id_componente) {
    var selectToSort = jQuery('#' + id_componente);
    var optionActual = selectToSort.val();
    selectToSort.html(selectToSort.children('option').sort(function (a, b) {
        return a.text === b.text ? 0 : a.text < b.text ? -1 : 1;
    })).val(optionActual);
}


function RemoverItem(Codigo, rec = "", desc = "") {

    var qtd = parseInt($("#qtdAtivos" + rec).val())
    $("#qtdAtivos" + rec).val(qtd - 1);
    var Tabela = document.getElementById("tableItensTransferencia" + rec);
    var Linhas = Tabela.getElementsByTagName('tr');

    if (Linhas.length == 2) {
        $("#tableItensTransferencia" + rec + " tr").remove();
        $("#tbbItensTransferencia" + rec).hide(300);

        $("#tableConfirmarItens tr").remove();
        $("#tbbConfirmarItens").hide(300);

    }
    else {
        $('table#tableItensTransferencia' + rec + ' tr#' + Codigo).remove();
        $('table#tableConfirmarItens tr#prov' + Codigo).remove();

    }

    if (rec == "")
        SegundaEtapa();
    else {
        var cbbTransfAtivos = document.getElementById("cbbAtivosRec");


        var opt = document.createElement("option");
        opt.value = Codigo;
        opt.text = desc;
        cbbTransfAtivos.add(opt, cbbTransfAtivos.options[cbbTransfAtivos.options.length]);

        ordenarSelect('cbbAtivosRec');
        $('#cbbAtivosRec').selectpicker('refresh');

        VerificaGravaRec();
    }

};
document.querySelector('.custom-file-input').addEventListener('change', function (e) {
    var fileName = document.getElementById("fuArquivo").files[0].name;
    var nextSibling = e.target.nextElementSibling;
    nextSibling.innerText = fileName.substring(0,8);
});

function VerificaGravaRec() {
    var cbbTransfAtivos = document.getElementById("cbbAtivosRec");

    if (cbbTransfAtivos.options.length <= 1)
        $('#gravaRec').show();
    else
        $('#gravaRec').hide();
}

function SalvarFotos(rec = "") {

    var arquivos = document.getElementById("fuArquivo" + rec);
    var txr2 = "";
    if (arquivos.files.length > 0) {
        if (rec == 'Rec')
            document.getElementById('btnSalvarFotosReceber').innerHTML = '<div class="spinner-border text-primary" role="status"><span class="sr-only" > Loading...</span></div>';
        else {
            document.getElementById('btnSalvarFotos').innerHTML = '<div class="spinner-border text-primary" role="status"><span class="sr-only" > Loading...</span></div>';
        }
        $("#divLoading").show(400);

        var formData = new FormData();
        formData.append("id", "1");
        formData.append("nome", "foto");

        for (var i = 0; i < arquivos.files.length; i++) {
            if (arquivos.files[i].size > 0) {
                formData.append("arquivo" + i, arquivos.files[i]);
            }
        }

        var qtd = parseInt($("#qtdAtivos" + rec).val())
        $("#qtdAtivos" + rec).val(qtd + 1);

        $.ajax({
            type: 'POST',
            url: '/Ativo/ReceberDados',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            async: false,
            success: function (response) {
                $.each(response, function () {
                    if (this.id >= 0) {
                        txr2 = 'data:image/jpg;base64,' + this.dados;
                    }
                    if (this.id == -1) {
                        Mensagem("divAlerta" + rec, this.dados);
                    }

                    if (this.id == -2) {
                        Mensagem("divAlerta" + rec, this.dados);
                    }

                    if (rec == '') {
                        document.getElementById('btnSalvarFotos').innerHTML = 'Adicionar';
                    }
                    else {
                        document.getElementById('btnSalvarFotosReceber').innerHTML = 'Adicionar';
                    }
                    $("#divLoading").hide();
                });
            },
            error: function (error) {
                alert(error);
                $("#divLoading").hide(2000);
            }
        });
    }
    SegundaEtapa();
    return txr2;
};

function Mensagem(div, msg) {
    $("#" + div).html(msg);
    $("#" + div).show(300);
    $("#" + div).delay(5000);
    $("#" + div).hide(300);
};

function PrimeiraEtapa() {
    var Motivo = $("#cbbMotivo").val();
    var Descricao = $("#txtDescricao").val();
    var FilialOrigem = $("#cbbFilialOrigem").val();
    var FilialDestino = $("#cbbFilialDestino").val();

    if (Motivo != "" && Descricao != "" && FilialOrigem != "" && FilialDestino != "") {
        document.getElementById('ativos').classList.remove('disabled');
        document.getElementById("btProx1").disabled = false;
        document.getElementById("btProx1").style.removeProperty('pointer-events');

    }
    else {
        document.getElementById('ativos').classList.add('disabled');
        document.getElementById('documentos').classList.add('disabled');
        document.getElementById('confirmar').classList.add('disabled');
        document.getElementById("btProx1").disabled = true;
        $("#btProx1").css("pointer-events", "none");

    }

};
function SegundaEtapa() {
    var Tabela = document.getElementById("tableItensTransferencia");
    var Linhas = Tabela.getElementsByTagName('tr');


    if (Linhas.length > 0) {
        document.getElementById("btProx2").disabled = false;
        document.getElementById('documentos').classList.remove('disabled');
        document.getElementById("btProx2").style.removeProperty('pointer-events');

    }
    else {
        document.getElementById('documentos').classList.add('disabled');
        document.getElementById('confirmar').classList.add('disabled');
        document.getElementById("btProx2").disabled = true;
        $("#btProx2").css("pointer-events", "none");

    }
};
function MontarConfirmDocs(nome, extensao, Codigo = 0, Caminho) {




    var txt = '  <div class="col-lg-3" id="docsConfi' + $("#txtQtd").val() + '">\
                                        <div class="form-group">\
                                            <div class="card shadow p-3 " style="border-top:10px">';

    if (Codigo != 0)
        txt += '<a href="'+Caminho+'">'


    if (extensao == '.doc' || extensao == '.docx' || extensao == '.txt')
        txt += '<img style="width: 7rem; height: 7rem;" src="https://lh3.googleusercontent.com/2Cl_8FmUmMFqBkl4Z8JhHbioccUgjfW9y4njhUYKHXgQ4triTqSVA0lbgaXc5FEHBLA=s180" class="card-img-top" alt="...">';
    else
        txt += '<img  style="width: 7rem; height: 7rem;" src="https://lh3.googleusercontent.com/W1Jwfw3dKIo8BsQFaLc0y4UflpgSUlDKiWn4LgjKXFW1Uxj1t8qfwYu987CnBDWdsENT" class="card-img-top" alt="...">';

    txt += '<div class="card-body"><p style="word-wrap: break-word;">' + nome + '</p>\
                                                            </div>';
    if (Codigo != 0)
        txt += '</a>';
    txt += '\
                                            </div>\
                                        </div>\
                                </div>';

    return txt;

};

function SalvarDocs() {
    document.getElementById('btnSalvarDocumentos').innerHTML = '<div class="spinner-border text-primary" role="status"><span class="sr-only" > Loading...</span></div>';

    var arquivos = document.getElementById("fuArquivoDoc");
    var txr2;
    var formData = new FormData();
    formData.append("id", $("#txtId").val());
    for (var i = 0; i < arquivos.files.length; i++) {
        if (arquivos.files[i].size > 0) {
            formData.append("arquivo" + i, arquivos.files[i]);
        }
    }

    $.ajax({
        type: 'POST',
        url: '/Transferencia/ReceberDados',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        async: false,
        success: function (response) {
            var txt = "";
            $.each(response, function () {
                if (this.id >= 0) {
                    $("#txtQtd").val(parseInt($("#txtQtd").val()) + 1);

                    txr2 = this.dados;

                    var imgs = $("#meuAnexoHidden").val();

                    if (imgs != "")
                        imgs += "**Separdor Doc**";

                    imgs += txr2;

                    //$("#meuAnexoHidden").val(imgs);

                    var txt = '  <div class="col-lg-2" id="docs' + $("#txtQtd").val() + '">\
                                        <div class="form-group">\
                                            <div class="card shadow p-3 " style="border-top:10px">\
                                                    <input type="hidden" value="'+ this.dados + '"  id="Doc' + $("#txtQtd").val() + '"/>\
                                                    <input type="hidden" value="'+ this.extensao + '"  id="Extensao' + $("#txtQtd").val() + '"/>\
                                                    <input type="hidden" value="'+ this.content + '"  id="Content' + $("#txtQtd").val() + '"/>\
                                                    <input type="hidden" value="'+ this.nome + '"  id="NomeDoc' + $("#txtQtd").val() + '"/>\
                                                    <input type="hidden" value="'+ this.tamanho + '"  id="Tamanho' + $("#txtQtd").val() + '"/>';

                    if (this.extensao == '.doc' || this.extensao == '.docx' || this.extensao == '.txt')
                        txt += '<img style="width: 7rem; height: 7rem;" src="https://lh3.googleusercontent.com/2Cl_8FmUmMFqBkl4Z8JhHbioccUgjfW9y4njhUYKHXgQ4triTqSVA0lbgaXc5FEHBLA=s180" class="card-img-top" alt="...">';
                    else
                        txt += '<img  style="width: 7rem; height: 7rem;" src="https://lh3.googleusercontent.com/W1Jwfw3dKIo8BsQFaLc0y4UflpgSUlDKiWn4LgjKXFW1Uxj1t8qfwYu987CnBDWdsENT" class="card-img-top" alt="...">';

                    txt += '<div class="card-body"><p>' + this.nome + '</p>\
                                                                <button type="button" class="btn btn-outline-danger btn-sm" onclick="javascript: ExcluirDoc('+ $("#txtQtd").val() + ')"><i class="fas fa-trash"></i></button>\
                                                            </div>\
                                            </div>\
                                        </div>\
                                </div>';

                    $("#cardDocs").show();



                    $("#Doc").append(txt)

                    $("#tbbConfirmarDocs").show();
                    $("#confirmarDocs").append(MontarConfirmDocs(this.nome, this.extensao));

                    //document.getElementById("imagem").innerHTML = txt;
                }
                if (this.id == -1) {
                    Mensagem("divAlertaDoc", this.dados);
                }

                if (this.id == -2) {
                    Mensagem("divAlertaDoc", this.dados);
                }
                document.getElementById('btnSalvarDocumentos').innerHTML = 'Adicionar';

            });

            if (parseInt($("#txtQtd").val()) > 0)
                $("#modalDocs").show();
        },
        error: function (error) {
            alert(error);
        }
    });

};


function ExcluirDoc(Codigo) {       //mexer aqui pra remover item confirmar 
    var doc = document.getElementById('docs' + Codigo);

    if (doc != null) {
        doc.remove();
        var docsConfi = document.getElementById('docsConfi' + Codigo).remove();

        $("#txtQtd").val(parseInt($("#txtQtd").val()) - 1);

        document.getElementById("fuArquivoDoc").value = "";
        document.getElementById("labelFoto").innerHTML = 'Selecione um Arquivo';

        var bases = $("#meuAnexoHidden").val().split("**Separdor Doc**");
        var novo = "";

        for (var i = 0; i < bases.length - 1; i++) {
            if (i != Codigo - 1) {

                novo += bases[i];

                if ($("#txtQtd").val() > 1)
                    novo += "**Separdor Doc**";
            }
        }

        if (bases.length - 1 != Codigo - 1)
            novo += bases[bases.length - 1];

        $("#meuAnexoHidden").val(novo);
    }
    var Imagem = document.getElementById('Doc');

    if (Imagem.childElementCount == 0) {
        $("#modalDocs").hide();
        $("#tbbConfirmarDocs").hide();
    }

};

function Gravar() {     //gravar transferencia

    var Tabela = document.getElementById("tableItensTransferencia");
    var Linhas = Tabela.getElementsByTagName('tr');
    var ativos = [];
    var imgs = [];
    var docs = [];
    var content = [];
    var nome = [];

    for (var i = 0; i < Linhas.length - 1; i++) {
        ativos.push($("#minhaImagem" + i).attr("alt"));
        imgs.push($("#minhaImagem" + i).attr("src"));
    }

    var qtdDoc = parseInt($("#txtQtd").val());

    for (var i = 1; i <= qtdDoc; i++) {
        docs.push($("#Doc" + i).val());
        content.push($("#Content" + i).val());
        nome.push($("#NomeDoc" + i).val());
    }

    var origem = document.getElementById("cbbFilialOrigem").value;
    var destino = document.getElementById("cbbFilialDestino").value;
    var motivo = document.getElementById("cbbMotivo").value;
    var descricao = $("#txtDescricao").val();


    $.ajax({
        type: 'POST',
        url: '/Transferencia/Gravar',
        data: { Origem: origem, Destino: destino, Motivo: motivo, Descricao: descricao, Docs: docs, Nome: nome, Content: content, Ativos: ativos, Imgs: imgs },
        async: false,
        success: function (result) {

            if (result == "") {
                $('#novaTransferencia').modal('hide');

                Swal.fire({
                    title: 'Sucesso',
                    type: 'success',
                    text: 'Transferência gerada e enviada para aprovação!',
                    timer: 5000
                })
            }
            else {
                Swal.fire({
                    title: 'Erro',
                    type: 'danger',
                    text: 'Erro ao Gravar',
                    timer: 5000
                })
            }

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(300);
        }
    });
    ObterTransferencias();
};



function ConfirmarCampos(dados = null) {
    $("#tbbConfirmarItens").show(1000);

    var Origem;
    var ResponsavelOrigem;
    var Destino;
    var ResponsavelDestino;
    var Motivo;


    if (dados == null) {
        Origem = document.getElementById("cbbFilialOrigem").options[document.getElementById("cbbFilialOrigem").selectedIndex].text;
        ResponsavelOrigem = document.getElementById('txtResponsavelOrigem').value;
        Destino = document.getElementById("cbbFilialDestino").options[document.getElementById("cbbFilialDestino").selectedIndex].text;
        ResponsavelDestino = document.getElementById('txtResponsavelDestino').value;
        Motivo = document.getElementById("cbbMotivo").options[document.getElementById("cbbMotivo").selectedIndex].text;
    }
    else {
        Origem = dados.filialOrigem;
        ResponsavelOrigem = dados.respOrigem;
        Destino = dados.filialDestino;
        ResponsavelDestino = dados.respDestino;
        Motivo = dados.motivo;

        var cbbTransfAtivos = document.getElementById("cbbTransfAtivos");
        for (var i = 0; dados.ativos != null && dados.ativos != undefined && i < dados.ativos.length; i++) {
            if (i == 0)         //montar itens ativos ... 
                $("#tableConfirmarItensAprov").html(MontarTableConfirmar(dados.ativos[i], dados.ativos[i].imagem, true));
            else
                $("#tableConfirmarItensAprov tbody").append(MontarTableConfirmar(dados.ativos[i], dados.ativos[i].imagem, true));
        }

        $("#tbbConfirmarItensAprov").show();
        if (dados.docs != null && dados.docs != undefined && dados.docs.length > 0) {
            $("#ConfirmarDocsAprov").show();
            $("#tbbConfirmarDocsAprov").show();
        }



        for (var i = 0; dados.docs != null && dados.docs != undefined && i < dados.docs.length; i++) {  //montar documentos ativos...
            var aux = dados.docs[i].nome.split(".");
            var extensao = '.' + aux[aux.length - 1]
            $("#confirmarDocsAprov").append(MontarConfirmDocs(dados.docs[i].nome, extensao, dados.docs[i].codigo, dados.docs[i].caminho));
        }
    }
    var txt = '';

    txt += '\
        <h6> Origem</h6>\
            <div class="row">\
                <div class="col-lg-6">\
                    <div class="form-group">\
                        <label>Filial de Origem:<b>'+ Origem + '</b></label>\
                    </div>\
                </div>\
                <div class="col-lg-6">\
                    <div class="form-group">\
                        <label>Responsavel Origem:<b>'+ ResponsavelOrigem + '</b></label>\
                    </div>\
                </div>\
            </div>\
            <hr />\
            <h6>Destino</h6>\
            <div class="row">\
                <div class="col-lg-6">\
                    <div class="form-group">\
                        <label>Filial de Destino:<b>'+ Destino + '</b></label>\
                    </div>\
                </div>\
                <div class="col-lg-6">\
                    <div class="form-group">\
                        <label>Responsavel Destino:<b>'+ ResponsavelDestino + '</b></label>\
                    </div>\
                </div>\
            </div>';

    if (dados == null)
        document.getElementById('confirmarCampos').innerHTML = txt;
    else {
        document.getElementById('confirmarCamposAprov').innerHTML = txt;


        if (dados.aprovGerente != 0) {
            var htm = ' <hr />\
                <h6> Aprovação Gerente</h6>\
            <div class="row">\
                        <div class="col-lg-6" >\
                                <div class="form-group">\
                                    <label>Data Aprovação:<b> '+ Data(dados.dtAprovGerente) + '</b></label>\
                                </div>\
                            </div>\
                            <div class="col-lg-6">\
                                <div class="form-group">\
                                    <label>Gerente Responsavel:<b> '+ dados.aprovanteGerente + '</b></label>\
                                </div>\
                        </div>\
                  </div>\
                <div class="row">\
                        <div class="col-lg-6" >\
                                <div class="form-group">\
                                    <label>Observação da Aprovação:<b> '+ dados.obsAprovGerente + '</b></label>\
                                </div>\
                            </div>\
                </div > ';

            $("#InfoAprovGerente").html(htm);
        }
        else {
            $("#InfoAprovGerente").html('');
        }

        if (dados.aprovDestino != 0) {



            var htm = ' <br><hr />\
                <h6>Recebimento</h6>\
            <div class="row">\
                        <div class="col-lg-6" >\
                                <div class="form-group">\
                                    <label>Data Aprovação:<b> '+ Data(dados.dtAprovDestino) + '</b></label>\
                                </div>\
                            </div>\
                            <div class="col-lg-6">\
                                <div class="form-group">\
                                    <label>Responsavel:<b> '+ dados.aprovanteDestino + '</b></label>\
                                </div>\
                        </div>\
                  </div>\
                <div class="row">\
                        <div class="col-lg-6" >\
                                <div class="form-group">\
                                    <label>Observação da Aprovação:<b> '+ dados.obsAprovDestino + '</b></label>\
                                </div>\
                            </div>\
                </div > ';

            $("#InfoAprovGerente").append(htm);
        }
    }
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
            "bFilter": true,
            columnDefs: [
                {
                    targets: 0,
                    createdCell: function (td, cellData, rowData, row, col) {
                        $(td).css('border-radius', '4px');
                        if (rowData[3] == '<span class="badge badge-primary">Novo</span>')
                            $(td).css('border-left', '4px solid blue');
                        else {
                            if (rowData[3] == '<span class="badge badge-success">Aprovado Origem</span>') {
                                $(td).css('border-left', '4px solid green');
                            }
                            else {
                                $(td).css('border-left', '4px solid #138496');
                            }
                        }
                    }
                }
            ]
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
            "bFilter": true,
            columnDefs: [
                {
                    targets: 0,
                    createdCell: function (td, cellData, rowData, row, col) {
                        $(td).css('border-radius', '4px');
                        if (rowData[3] == '<span class="badge badge-primary">Novo</span>')
                            $(td).css('border-left', '4px solid blue');
                        else {
                            if (rowData[3] == '<span class="badge badge-success">Aprovado Origem</span>') {
                                $(td).css('border-left', '4px solid green');
                            }
                            else {
                                $(td).css('border-left', '4px solid #138496');
                            }
                        }
                    }
                }
            ]
        });
    }
};

function Rolagem() {
    var $anchor = $(this);
    $('html, body').stop().animate({
        scrollTop: ($($anchor.attr('tbbTransferencia')).offset().top)
    }, 1000, 'easeInOutExpo');
};

function Data(data) {
    if (data != '01/01/1900 00:00:00' && data != '1/1/1900 12:00:00 AM')
        return data.replace(/(\d*)-(\d*)-(\d*).*/, '$3-$2-$1').replace('-', '/').replace('-', '/').substring(0, 10);
    else
        return ' ';
};

function StatusTransf(DataRecebimento, DataAprovacao) {
    if (DataRecebimento == ' ' && DataAprovacao == ' ') {
        return '<span class="badge badge-primary">Novo</span>';
    }
    else {
        if (DataAprovacao != ' ' && DataRecebimento != ' ')
            return '<span class="badge badge-info">Aprovado Destino</span>';
        else {
            if (DataAprovacao != ' ')
                return '<span class="badge badge-success">Aprovado Origem</span>';
        }

    }
};
function PreencherTabela(dados) {
    var regiao = parseInt($("#regional").val());
    var filial = parseInt($("#filial").val());

    var i = 0;
    $("#tbbTransferencia").show(300);
    var txt = '<thead>\
            <tr class="thead-light">\
                <th scope="col">Origem</th>\
                <th scope="col">Destino</th>\
                <th scope="col">Motivo</th>\
                <th scope="col">Status</th>\
                <th scope="col">Data Abertura</th>\
                <th scope="col" style="min-width:150px; width:250px"></th>\
            </tr>\
        </thead >\
        <tbody>';

    $.each(dados, function () {


        this.dtRecebimento = this.dtRecebimento == '01/01/1900 00:00:00' || this.dtRecebimento == '1/1/1900 12:00:00 AM' ? ' ' : this.dtRecebimento;
        this.dtAprovacao = this.dtAprovacao == '01/01/1900 00:00:00' || this.dtAprovacao == '1/1/1900 12:00:00 AM' ? ' ' : this.dtAprovacao;

        txt += '<tr><td>' + this.razaoOrigem + '</td><td>' + this.razaoDestino + '</td><td>' + this.motivoDesc + '</td><td>' + StatusTransf(this.dtRecebimento, this.dtAprovacao) + '</td><td>' + Data(this.dtAbertura) + '</td><td align="right" class="form-group">'
        txt += '<button role="button" class="btn btn-info" href="#" onclick="javascript:MostraTransf(' + this.codigo + ',0);" data-toggle="modal" data-target="#VisualizarTransf" title="Visualizar"><i class="fas fa-eye"></i> <span>Visualizar</span></button> ';

        if (filial != 0) {

            if (this.dtRecebimento == " " && this.dtAprovacao != " ") {
                txt += '<button role="button" class="btn btn-success" href="#" onclick="javascript:LimparReceber(); alterar(' + this.codigo + ');MostraTransf(' + this.codigo + ',1);" data-toggle="modal" data-target="#ReceberTransf" title="Receber"><i class="fas fa-check"></i> <span>Receber</span></button>';
            }

        }
        else {
            if (regiao != 0) {
                if (this.dtAprovacao == " ")
                    txt += '<button role="button" class="btn btn-warning" onclick="javascript:alterar(' + this.codigo + ');" data-toggle="modal" data-target="#AprovarTransf" title="Aprovar"><i class="fas fa-check-circle"></i> <span>Aprovar</span></button>';

            }
            else {
                if (this.dtAprovacao == " ") {
                    txt += '<button role="button" class="btn btn-warning" onclick="javascript:alterar(' + this.codigo + ');" data-toggle="modal" data-target="#AprovarTransf" title="Aprovar"><i class="fas fa-check-circle"></i> <span>Aprovar</span></button>';
                }
                else
                    if (this.dtRecebimento == " ")
                        txt += '<button role="button" class="btn btn-success" href="#" onclick="javascript: LimparReceber(); alterar(' + this.codigo + ',1); MostraTransf(' + this.codigo + ',1);" data-toggle="modal" data-target="#ReceberTransf" title="Receber"><i class="fas fa-check"></i> <span>Receber</span></button>';
            }
        }


        txt += '</td></tr>';
        i++;
    });
    txt += '</tbody>';
    $("#tableTransferencia").html(txt);
    funcaoTable("#tableTransferencia");
    Rolagem();
}

function alterar(cod) {
    $("#HcdTransfAprov").val(cod);
}

function Receber(dados) {   //preencher combo receber ativos...

    LimparCombo('cbbAtivosRec');
    LimparCombo('cbbSalaDestino');
    $("#ReceberAtivoDestino").show(300);
    BuscarSalasReceber(dados.codigoFilialDestino);

    var cbbTransfAtivos = document.getElementById("cbbAtivosRec");
    for (var i = 0; dados.ativos != null && dados.ativos != undefined && i < dados.ativos.length; i++) {

        var opt = document.createElement("option");
        opt.value = dados.ativos[i].codigo;
        opt.text =dados.ativos[i].placa+'-'+ dados.ativos[i].descricao;
        cbbTransfAtivos.add(opt, cbbTransfAtivos.options[i]);
    }
    $('#cbbAtivosRec').selectpicker('refresh');
    $('#cbbSalaDestino').selectpicker('refresh');

};

function ObterTransferencias() {
    $("#divLoading").show(300);
    var Origem = document.getElementById('cbbFilialOrigemPesquisa').value;
    var Destino = document.getElementById('cbbFilialDestinoPesquisa').value;
    var Ativo = document.querySelector('input[name="rdAtivo"]:checked').value;
    var Radio = document.getElementsByName("rdAtivo");
    var Regiao = parseInt($("#regional").val());
    var Filial = parseInt($("#filial").val());

    var Texto = "";

    $.ajax({
        type: 'POST',
        url: '/Transferencia/ObterTransferencias',
        data: { Origem: Origem, Destino: Destino, Ativo: Ativo, Regiao: Regiao, Filial: Filial },
        async: false,
        success: function (result) {
            if (result != null && result.length > 0) {
                PreencherTabela(result);
            }
            else {
                Texto = "Nenhuma Transferência Encontrada!";

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

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(300);
        }
    });
};

function GravarAprovacao() {        //gravar aprovação origem...
    $("#divLoading").show(300);

    var Transf = $("#HcdTransfAprov").val();
    var Obs = $("#txtObsAprov").val();


    $.ajax({
        type: 'POST',
        url: '/Transferencia/Aprovar',
        data: { Transf: Transf, Obs: Obs },
        success: function (result) {
            if (result == "") {
                $('#AprovarTransf').modal('hide');

                Swal.fire({
                    title: 'Sucesso',
                    type: 'success',
                    text: 'Transferência Aprovada!',
                    timer: 5000
                })
                ObterTransferencias();
                $("#txtObsAprov").val("");
            }
            else {
                Swal.fire({
                    title: 'Erro',
                    type: 'danger',
                    text: 'Erro ao Gravar',
                    timer: 5000
                })
                ObterTransferencias();

            }

            $("#divLoading").hide(300);
            ObterTransferencias();

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(300);
            ObterTransferencias();

        }
    });

};


function MostraTransf(transf, rec = 0) {
    $("#tableConfirmarItensAprov").html('');
    $("#confirmarDocsAprov").html('');
    $("#tbbConfirmarItensAprov").hide();
    $("#tbbConfirmarDocsAprov").hide();
    $("#divLoading").show(400);

    //$("#HcdTransfAprov").val(transf);


    $.ajax({
        type: 'POST',
        url: '/Transferencia/BuscarTransferencia',
        data: { Codigo: transf },
        success: function (result) {
            if (result != "" && result != null) {

                if (rec != 0) {
                    Receber(result);
                    $("#divLoading").hide(1400);
                }
                else {
                    ConfirmarCampos(result);
                    $("#divLoading").hide(1400);
                }
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(1400);
        }
    });
}

function GravarRec() {      //gravar recebimento destino...
   var latitude = 0;
    var longitude = 0;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function Resp(position) {
             latitude = position.coords.latitude;
             longitude = position.coords.longitude;


            if (latitude != 0) {
                var Tabela = document.getElementById("tableItensTransferenciaRec");
                var Linhas = Tabela.getElementsByTagName('tr');
                var ativos = [];
                var imgs = [];
                var salas = [];

                for (var i = 0; i < Linhas.length - 1; i++) {
                    ativos.push($("#minhaImagemRec" + i).attr("alt"));
                    imgs.push($("#minhaImagemRec" + i).attr("src"));
                    salas.push($("#salaDestino" + i).val());
                }

                

                

                var obs = $("#txtObsRec").val();
                var transf = $("#HcdTransfAprov").val();

                $.ajax({
                    type: 'POST',
                    url: '/Transferencia/Receber',
                    async: false,
                    data: { Codigo: transf, Obs: obs, Ativos: ativos, Imgs: imgs, Latitude: latitude, Longitude: longitude, Salas: salas },
                    success: function (result) {

                        if (result == "") {
                            $('#ReceberTransf').modal('hide');

                            Swal.fire({
                                title: 'Sucesso',
                                type: 'success',
                                text: 'Transferência recebida com Sucesso!',
                                timer: 5000
                            })
                        }
                        else {
                            Swal.fire({
                                title: 'Erro',
                                type: 'danger',
                                text: 'Erro ao Gravar',
                                timer: 5000
                            })
                        }

                    },
                    error: function (XMLHttpRequest, txtStatus, errorThrown) {
                        alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                        $("#divLoading").hide(300);
                    }
                });


                ObterTransferencias();
            }
            else
                Swal.fire({
                    title: 'Erro',
                    type: 'danger',
                    text: 'Erro ao obter Posição',
                    timer: 5000
                })

        });
    }
    else
        Swal.fire({
            title: 'Erro',
            type: 'danger',
            text: 'Erro ao obter Posição',
            timer: 5000
        })
};

function BotoesProximo(Atual, Proximo, NavAtual, NavProx) {
    document.getElementById(Atual).classList.remove('active');
    document.getElementById(Proximo).classList.add('active');

    document.getElementById(NavAtual).classList.remove('show');
    document.getElementById(NavProx).classList.add('show');

    document.getElementById(NavAtual).classList.remove('active');
    document.getElementById(NavProx).classList.add('active');
};

function BuscarSalasReceber(Codigo) {

    if (Codigo > 0) {
        var cbbLocalDestino = document.getElementById("cbbSalaDestino");
        if (cbbLocalDestino != null) {

            $.ajax({
                type: 'POST',
                url: '/Sala/BuscarSalas',
                async: false,
                data: { Codigo: Codigo },
                success: function (result) {
                    if (result != null && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            $('#cbbSalaDestino').append('<option value="' + result[i].codigo + '">' + result[i].descricao + '</option>');

                        }
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(300);
                }
            });
        }
    }
    $('#cbbSalaDestino').selectpicker('refresh');
};
function BuscarSalas(Codigo) {
    
    if (Codigo > 0) {
        var cbbLocalDestino = document.getElementById("cbbSalaDestino");
        if (cbbLocalDestino != null) {

            $.ajax({
                type: 'POST',
                url: '/Sala/BuscarSalas',
                async: false,
                data: { Codigo: Codigo },
                success: function (result) {
                    if (result != null && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            $('#cbbSalaDestino').append('<option value="' + result[i].codigo + '">' + result[i].descricao + '</option>');

                        }
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                    $("#divLoading").hide(300);
                }
            });

            ObterResponsavel(Codigo, 2);
        }

    }
    else {
    }
    $('#cbbSalaDestino').selectpicker('refresh');
};

document.getElementById('fuArquivoRec').addEventListener('change', function (e) {
    var fileName = document.getElementById("fuArquivoRec").files[0].name;
    var nextSibling = e.target.nextElementSibling;
    nextSibling.innerText = fileName.substring(0, 8);
});
