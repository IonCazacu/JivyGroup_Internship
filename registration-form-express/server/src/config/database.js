const { Sequelize } = require('sequelize');
const path = require('path');


const sequelize = new Sequelize('User', 'user', 'password', {
  dialect:'sqlite',
  host: path.join('server', 'src', 'config', 'dev.sqlite')
});


module.exports = sequelize;
