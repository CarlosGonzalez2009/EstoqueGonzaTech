
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
    </style>
</head>
<body>
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

    <script>
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

        // Event listener para submissão do formulário
        document.getElementById('addItemForm').addEventListener('submit', addItem);

        // Renderiza os itens ao carregar a página
        window.onload = renderItems;
    </script>
</body>
</html>

/* styles.css */

body {
  font-family: Arial, sans-serif;
  padding: 20px;
  background-color: #f0f0f0;
}

.container {
  max-width: 800px;
  margin: 0 auto;
  background-color: #fff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

h1 {
  text-align: center;
  margin-bottom: 20px;
}

form {
  margin-bottom: 20px;
}

form label {
  display: block;
  margin-bottom: 8px;
}

form input[type="text"],
form input[type="number"] {
  width: calc(100% - 70px); /* Ajusta a largura dos inputs para caber no form */
  padding: 8px;
  margin-bottom: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
}

form button {
  width: 100px;
  padding: 8px;
  border: none;
  background-color: #007bff;
  color: #fff;
  cursor: pointer;
  border-radius: 4px;
}

form button:hover {
  background-color: #0056b3;
}

.search-container {
  margin-bottom: 20px;
}

.search-container label {
  margin-right: 10px;
}

.search-container input[type="text"] {
  width: 300px;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

table th, table td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

table th {
  background-color: #f2f2f2;
}

button.clear-btn {
  padding: 10px 20px;
  background-color: #dc3545;
  color: #fff;
  border: none;
  cursor: pointer;
  border-radius: 4px;
}

button.clear-btn:hover {
  background-color: #c82333;
}







