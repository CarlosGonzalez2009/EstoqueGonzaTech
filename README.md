<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Controle de Estoque</title>
    <link rel="stylesheet" href="carlos.css">
    <style>
        /* Estilos opcionais */
        body {
            font-family: Arial, sans-serif;
            padding: 20px;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        th, td {
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
        }
        th {
            background-color: #f2f2f2;
        }
        .search-container {
            margin-top: 10px;
        }
        .password-container {
            display: none; /* Inicia oculto */
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Fundo escuro semitransparente */
            z-index: 1000;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .password-box {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
            text-align: center;
        }
        .password-input {
            padding: 8px;
            margin-bottom: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
            width: 200px;
        }
        .password-submit {
            padding: 8px 20px;
            background-color: #007bff;
            color: #fff;
            border: none;
            cursor: pointer;
            border-radius: 4px;
        }
        .password-submit:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <!-- Container para solicitar senha -->
    <div id="passwordContainer" class="password-container">
        <div class="password-box">
            <h2>Informe a senha para acessar:</h2>
            <input type="password" id="passwordInput" class="password-input">
            <button onclick="checkPassword()" class="password-submit">Acessar</button>
        </div>
    </div>

    <!-- Conteúdo principal -->
    <div id="mainContent" style="display: none;">
        <h1>Controle de Estoque</h1>

        <form id="addItemForm">
            <label for="itemName">Nome do Item:</label>
            <input type="text" id="itemName" required>
            <label for="itemQuantity">Quantidade:</label>
            <input type="number" id="itemQuantity" required>
            <button type="submit">Adicionar Item</button>
        </form>

        <div class="search-container">
            <label for="searchItem">Pesquisar Produto:</label>
            <input type="text" id="searchItem" oninput="searchItem()" placeholder="Digite o nome do produto...">
        </div>

        <table id="itemList">
            <thead>
                <tr>
                    <th>Nome do Item</th>
                    <th>Quantidade</th>
                    <th>Cliente</th>
                    <th>Status</th>
                    <th>Ações</th>
                </tr>
            </thead>
            <tbody>
                <!-- Os itens serão adicionados aqui dinamicamente -->
            </tbody>
        </table>

        <button onclick="clearStock()">Limpar Estoque</button>
    </div>

    <script>
        // Função para verificar a senha
        function checkPassword() {
            var password = document.getElementById('passwordInput').value;
            if (password === '130102') {
                // Senha correta, exibe o conteúdo principal e oculta o container da senha
                document.getElementById('passwordContainer').style.display = 'none';
                document.getElementById('mainContent').style.display = 'block';
                // Inicializa a aplicação
                initializeApp();
            } else {
                alert('Senha incorreta. Tente novamente.');
            }
        }

        // Função para inicializar a aplicação após autenticação
        function initializeApp() {
            // Verifica se há itens salvos no localStorage ao carregar a página
            renderItems();

            // Event listener para submissão do formulário
            document.getElementById('addItemForm').addEventListener('submit', addItem);

            // Renderiza os itens ao carregar a página
            window.onload = renderItems;
        }

        // Função para adicionar um item ao estoque
        function addItem(event) {
            event.preventDefault();

            // Captura os valores do formulário
            const itemName = document.getElementById('itemName').value;
            const itemQuantity = document.getElementById('itemQuantity').value;

            // Cria um objeto para representar o item
            const item = {
                name: itemName,
                quantity: parseInt(itemQuantity),
                cliente: '',
                status: 'No Estoque' // Adicionamos um status inicial
            };

            // Limpa o formulário
            document.getElementById('addItemForm').reset();

            // Adiciona o item ao armazenamento local (localStorage)
            let items = JSON.parse(localStorage.getItem('items')) || [];
            items.push(item);
            localStorage.setItem('items', JSON.stringify(items));

            // Atualiza a tabela de itens
            renderItems();
        }

        // Função para marcar um item como vendido ou no estoque
        function toggleStatus(index) {
            let items = JSON.parse(localStorage.getItem('items')) || [];

            // Alterna entre "No Estoque" e "Vendido"
            if (items[index].status === 'No Estoque') {
                items[index].status = 'Vendido';
                items[index].quantity--; // Reduz a quantidade quando marcado como vendido
                const cliente = prompt('Digite o nome do cliente:');
                items[index].cliente = cliente || '';
            } else {
                items[index].status = 'No Estoque';
            }

            localStorage.setItem('items', JSON.stringify(items));

            // Atualiza a tabela de itens
            renderItems();
        }

        // Função para limpar todo o estoque
        function clearStock() {
            localStorage.removeItem('items');
            renderItems(); // Atualiza a tabela após limpar o estoque
        }

        // Função para renderizar os itens na tabela
        function renderItems() {
            const itemList = document.getElementById('itemList').getElementsByTagName('tbody')[0];
            itemList.innerHTML = '';

            // Obtém os itens do armazenamento local
            let items = JSON.parse(localStorage.getItem('items')) || [];

            // Adiciona cada item na tabela
            items.forEach((item, index) => {
                const row = itemList.insertRow();
                row.innerHTML = `<td>${item.name}</td>
                                <td>${item.quantity}</td>
                                <td>${item.cliente}</td>
                                <td>${item.status}</td>
                                <td><button onclick="toggleStatus(${index})">${item.status === 'No Estoque' ? 'Marcar Vendido' : 'Marcar no Estoque'}</button></td>`;
            });
        }

        // Função para pesquisar itens por nome
        function searchItem() {
            const searchText = document.getElementById('searchItem').value.toLowerCase();
            const itemList = document.getElementById('itemList').getElementsByTagName('tbody')[0];

            // Obtém os itens do armazenamento local
            let items = JSON.parse(localStorage.getItem('items')) || [];

            // Filtra os itens que correspondem à pesquisa
            let filteredItems = items.filter(item => item.name.toLowerCase().includes(searchText));

            // Atualiza a tabela de itens com os resultados da pesquisa
            itemList.innerHTML = '';
            filteredItems.forEach(item => {
                const row = itemList.insertRow();
                row.innerHTML = `<td>${item.name}</td>
                                <td>${item.quantity}</td>
                                <td>${item.cliente}</td>
                                <td>${item.status}</td>
                                <td><button onclick="toggleStatus(${items.indexOf(item)})">${item.status === 'No Estoque' ? 'Marcar Vendido' : 'Marcar no Estoque'}</button></td>`;
            });
        }
    </script>
</body>
</html>
