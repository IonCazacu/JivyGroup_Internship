const { Sequelize } = require('sequelize');

const sequelize = new Sequelize('User', 'user', 'password', {
  dialect:'sqlite',
  host: './dev.sqlite'
});

module.exports = sequelize;
