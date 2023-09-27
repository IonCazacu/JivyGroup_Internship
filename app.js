const express = require('express');
const path = require('path');

const userRoutes = require(path.join(__dirname, 'server', 'src', 'routes', 'userRoutes'));
const sequelize = require(path.join(__dirname, 'server', 'src', 'config', 'database'));


sequelize
  .sync({
    force: true
  })
  .then(() => {
    console.log('db is ready')
  })
  .catch((error) => {
    console.log(`Unable to sync database: ${ error.message }`)
  });


const app = express();
const port = 8080;


app.use(express.static(path.join(__dirname, 'client', 'public')));
app.use(express.json());


app.use('/api', userRoutes);


app.get('/register', (req, res) => {
  const index = path.join(__dirname, 'client', 'public', 'views', 'register.html')
  res.sendFile(index);
})


app.listen(port, () => {
  console.log(`Application listening on port ${ port }`);
})
