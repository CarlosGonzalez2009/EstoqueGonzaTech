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
