const express = require('express');
const path = require('path');

const app = express();

app.use(express.static(path.join(__dirname, 'public')));

app.get('/', (req, res) => {
  res.sendFile(`${ __dirname }/views/registration-form.html`)
})

app.listen(3333, () => {
  console.log('Applcation listening on port 3333');
})
