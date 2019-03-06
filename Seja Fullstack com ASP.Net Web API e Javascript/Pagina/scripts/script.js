var tbody = document.querySelector('table tbody');
var aluno = {};

CarregaAlunos();

function NovoAluno() {
	limpar();

	var titulo = document.querySelector('#exampleModalLabel')

	titulo.textContent = 'Cadastrar Aluno'
	
	$('#exampleModal').modal('show')
}

function Cadastrar() {
	aluno.nome = document.getElementById('nome').value;
	aluno.sobrenome = document.getElementById('sobrenome').value;
	aluno.telefone = document.getElementById('telefone').value;
	aluno.ra = document.getElementById('ra').value;

	if (aluno.id === undefined || aluno.id === 0) 
	{
		SalvarAlunos('POST', 0, aluno);
	}
	else
	{
		SalvarAlunos('PUT', aluno.id, aluno);
	}

	CarregaAlunos();
}

function Cancelar() {
	limpar();

	var titulo = document.querySelector('#exampleModalLabel')

	titulo.textContent = 'Cadastrar Aluno'
	
	$('#exampleModal').modal('hide')
}

function CarregaAlunos() {

	tbody.innerHTML = '';

	var xhr = new XMLHttpRequest();
	
	xhr.open("GET", `http://localhost:12758/api/aluno`, true);

	xhr.onerror = function () {
		console.log("ERRO", xhr.readyState);
	}

	xhr.onreadystatechange = function () {
		if (this.readyState == 4) {
			if (this.status == 200) {
				var aluno = JSON.parse(this.responseText);
				for(var indice in aluno){
					AdicionaLinha(aluno[indice]);
				}
			}
			else if (this.status == 500) {
				var erro = JSON.parse(this.responseText);
				console.log(erro.Message);
				console.log(erro.ExceptionMessage);
			}
		}
	}
	
	xhr.send();
}

function SalvarAlunos(metodo, id, corpo) {

	var xhr = new XMLHttpRequest();
	
	if (id === undefined || id === 0) 
	{
		id = '';
	}

	xhr.open(metodo, `http://localhost:12758/api/aluno/${id}`, false);

	xhr.setRequestHeader('content-type', 'application/json');
	xhr.send(JSON.stringify(corpo));

	limpar();
	
	$('#exampleModal').modal('hide')
}

function AdicionaLinha(aluno) {
	var trow = `<tr>
					<td>${aluno.nome}</td>
					<td>${aluno.sobrenome}</td>
					<td>${aluno.telefone}</td>
					<td>${aluno.ra}</td>
					<td>
						<button class="btn btn-info" data-toggle="modal" data-target="#exampleModal" onclick='editarAluno(${JSON.stringify(aluno)})'>Editar</button>
						<button class="btn btn-danger" onclick='excluir(${aluno.id})'>Excluir</button>
					</td>
				</tr>`

	tbody.innerHTML += trow;
}

function editarAluno(_aluno) {
	var titulo = document.querySelector('#exampleModalLabel')

	document.getElementById('nome').value = _aluno.nome;
	document.getElementById('sobrenome').value = _aluno.sobrenome;
	document.getElementById('telefone').value = _aluno.telefone;
	document.getElementById('ra').value = _aluno.ra;
	
	titulo.textContent = `Editar Aluno ${_aluno.nome}`

	aluno = _aluno;
}

function excluirAluno(id) {
	var xhr = new XMLHttpRequest();
	
	xhr.open("DELETE", `http://localhost:12758/api/aluno/${id}`, false);

	xhr.send();
}

function excluir(id) {

	bootbox.confirm({
	    message: "Confirma exclusão do aluno?",
	    buttons: {
	        confirm: {
	            label: 'Sim',
	            className: 'btn-success'
	        },
	        cancel: {
	            label: 'Não',
	            className: 'btn-danger'
	        }
	    },
	    callback: function (result) {
	        if (result) 
        	{
				excluirAluno(id);
				CarregaAlunos();
        	}
	    }
	});
}

function limpar() {
	document.getElementById('nome').value = '';
	document.getElementById('sobrenome').value = '';
	document.getElementById('telefone').value = '';
	document.getElementById('ra').value = '';

	aluno = {};
}
