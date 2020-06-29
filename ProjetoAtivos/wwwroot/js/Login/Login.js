$(document).ready(function () {
    ExibirSenha();
    Calendario();
});

///////////////////////############Login############///////////////////////
function Login() {
    $("#divLoading").show(400);

    var Login = document.getElementById('txtUsuario').value;
    var Senha = document.getElementById('txtSenha').value;
    $.ajax({
        type: 'POST',
        url: '/Login/Login',
        data: { Login: Login, Senha: Senha },
        success: function (result) {
            if (result.length > 0) {
                if (result == 'Parametrização') {
                    window.location.href = '/Login/PrimeiroAcesso';
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: 'Erro!',
                        text: result,
                    })
                }     
            }
            else {
                window.location.href = '/Home/Index';
            }
            $("#divLoading").hide(400);
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
};

function EsqueceuSenha() {
    $.fancybox.close('esqueceuSenha');
    Swal.fire({
        title: 'Sucesso!',
        text: 'A Sua Nova Senha foi Enviada por email!',
        type: 'success',
        confirmButtonText: 'Cool'
    })
};

function MostrarSenha() {

    var key_attr = $('#txtSenha').attr('type');

    if (key_attr != 'text') {

        $('.checkbox').addClass('show');
        $('#txtSenha').attr('type', 'text');

    } else {

        $('.checkbox').removeClass('show');
        $('#txtSenha').attr('type', 'password');

    }
};

function LimparInputLogin() {
   /* document.GetElementById('txtUsuario').value = ' ';
    document.GetElementById('txtSenha').value = ' ';*/

};
///////////////////////############Primeiro Acesso############///////////////////////
function PrimeiroAcesso() {
    $("#divLoading").show(400);

    var Codigo = document.getElementById('txtCodigo').value;
    var Email = document.getElementById('txtUsuario').value;
    var Senha = document.getElementById('txtNovaSenha').value;

    var Nome = document.getElementById('txtNome').value;
    var Cpf = document.getElementById('txtCpf').value;
    var Rg = document.getElementById('txtRG').value;
    var DtNascimento = document.getElementById('dtNascimento').value;
    var Telefone = document.getElementById('txtFone1').value;
    var Telefone2 = document.getElementById('txtFone2').value;
    var Sexo = 'M';

    var Endereco = document.getElementById('txtEndereco').value;
    var Numero = document.getElementById('txtNumero').value;
    var Bairro = document.getElementById('txtBairro').value;
    var Cidade = document.getElementById('txtCidade').value;
    var Cep = document.getElementById('txtCEP').value;
    var Estado = document.getElementById('txtEstado').value;
    

    var NomeEmpresa = document.getElementById('txtEmpresa').value;
    var Cnpj = document.getElementById('txtCnpj').value; 
    var EndEmpresa = document.getElementById('txtEnderecoEmpresa').value;
    var NroEmpresa = document.getElementById('txtNroEmpresa').value;
    var BairroEmpresa = document.getElementById('txtBairroEmpresa').value; 
    var CidadeEmpresa = document.getElementById('txtCidadeEmpresa').value; 
    var EmailEmpresa = document.getElementById('txtEmailEmpresa').value;
    var TelefoneEmpresa = document.getElementById('txtTelefoneEmpresa').value;

    $.ajax({
        type: 'POST',
        url: '/Login/PrimeiroAcessoC',
        async: false,
        data: {
            Codigo: Codigo, Email: Email, Senha: Senha, Nome: Nome, Cpf: Cpf, Rg: Rg, DtNascimento: DtNascimento, Telefone: Telefone,
            Telefone2: Telefone2, Sexo: Sexo, Endereco: Endereco, Numero: Numero, Bairro: Bairro, Cidade: Cidade, Cep: Cep, Estado: Estado,
            NomeEmpresa: NomeEmpresa, Cnpj: Cnpj, EndEmpresa: EndEmpresa, NroEmpresa: NroEmpresa, BairroEmpresa: BairroEmpresa, CidadeEmpresa: CidadeEmpresa,
            EmailEmpresa: EmailEmpresa, TelefoneEmpresa: TelefoneEmpresa},
            success: function (result) {
                if (result.length > 0) {
                    Swal.fire({
                        title: result,
                        text: 'Você Será Redirencionado para o Primeiro Acesso',
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    }).then((result) => {
                        if (result.value) {
                            window.location.href = '/Login/PrimeiroAcesso';
                        }
                    })
                    
                }
                else {
                    Swal.fire({
                        title: 'Sucesso',
                        text: 'Você Será Redirencionado para area de Login',
                        type: 'success',
                        allowOutsideClick: false,
                        allowEscapeKey:false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    }).then((result) => {
                        if (result.value) {
                            window.location.href = '/Home/Index';
                        }
                    })
                    
                }
            
            $("#divLoading").hide(400);
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            $("#divLoading").hide(400);
        }
    });
};

function ValidarEmail() {
    var email = document.GetElementById('txtNovoUsu');
    var data = email.value;
    if (data.includes("@") && data.length > 5) {
        email.SetCustomValidity("");
        return true;
    }
    else {
        email.SetCustomValidity("");
        return false;
    }
};

function ExibirSenha() {
    var senha = $('#txtNovaSenha');
    var confsenha = $('#txtConfiNovaSenh');
    var olho = $("#olho");
    var olho2 = $("#olho2");

    olho.mousedown(function () {
        senha.attr("type", "text");
    });
    olho2.mousedown(function () {
        confsenha.attr("type", "text");
    })

    olho.mouseup(function () {
        senha.attr("type", "password");
    });
    olho2.mouseup(function () {
        confsenha.attr("type", "password");
    });

    $("#olho").mouseout(function () {
        $("#txtNovaSenha").attr("type", "password");
    });
    $("#olho2").mouseout(function () {
        $("#txtConfiNovaSenh").attr("type", "password");
    });
};

function Calendario() {
    var Calendar = document.querySelectorAll('.datepicker');
    M.Datepicker.init(Calendar, {});
};

function ValidarSenha() {
    var senha = document.getElementById('txtNovaSenha');
    var confSenha = document.getElementById('txtConfiNovaSenh');
    var botao = document.getElementById('btnProximo');
    if (senha.value == confSenha.value) {
        confSenha.setCustomValidity("");
        return true;
    }
    else {
        confSenha.setCustomValidity("Senhas diferentes!");
        return false;
    }
};

function ProximoPasso() {
    $("#usuario").hide(400);
    $('#pessoa').show(400); 
};
function PassoParametrizacao() {
    $('#pessoa').hide(400); 
    $('#parametriazao').show(400);
};
