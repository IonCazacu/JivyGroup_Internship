const express = require('express');


const userRoute = express.Router();
const userController = require('../controllers/userController');


userRoute.post('/users', userController.createUser);
userRoute.get('/users', userController.getUsers);
userRoute.get('/users/:id', userController.getUser);
userRoute.put('/users/:id', userController.updateUser)
userRoute.delete('/users/:id', userController.deleteUser);

module.exports = userRoute;
