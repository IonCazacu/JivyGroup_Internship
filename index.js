const express = require('express');
const path = require('path');
const sequelize = require('./database');
const User = require('./User');

sequelize.sync().then(() => console.log('db is ready')); 

const app = express();
const port = 8080;

app.use(express.static(path.join(__dirname, 'public')));
app.use(express.json());


app.get('/', (req, res) => {
  res.sendFile(`${ __dirname }/views/registration-form.html`)
})


app.post('/users', async (req, res) => {

  try {

    const user = await User.findOne({
      where: {
        email: req.body.email
      }
    });
  
    if (user === null) {
      
      await User.create(req.body);
      res.send(`Email ${ req.body.email } has been registered`);
  
    } else {
    
      res.send(`Email ${ req.body.email } is already registered`);
    
    }

  } catch (error) {

    console.error(error);

  }

});


app.get('/users', async (req, res) => {

  try {
    
    const users = await User.findAll();
    res.send(users);

  } catch (error) {

    console.log(error);

  }

});


app.listen(port, () => {
  console.log(`Application listening on port ${ port }`);
})
