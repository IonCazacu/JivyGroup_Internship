const UserService = require('../services/userService');


const UserController = {

  createUser: async (req, res) => {

    try {

      const userData = req.body;

      const newUser = await UserService.createUser(userData);
      res.json(newUser);

    } catch (error) {
  
      res.status(500).json({
        message: error.message
      });
  
    }

  },
  
  getUsers: async (req, res) => {

    try {
      
      const users = await UserService.getUsers();
      res.json(users)
  
    } catch (error) {
      
      res.status(500).json({
        message: error.message
      });
  
    }
    
  },

  getUser: async (req, res) => {

    try {

      const id = req.params.id;
      
      const user = await UserService.getUser(id);
      res.json(user)
      
    } catch (error) {
      
      res.status(500).json({
        message: error.message
      });

    }

  },

  updateUser: async (req, res) => {

    try {
      
      const id = req.params.id;
      const userData = req.body;
      
      const user = await UserService.updateUser(id, userData);
      res.json(user);

    } catch (error) {

      res.status(500).json({
        message: error.message
      });

    }

  },

  deleteUser: async (req, res) => {

    try {
      
      const id = req.params.id;
      
      const user = await UserService.deleteUser(id);
      res.status(200).json({
        message: 'User deleted successfully'
      })

    } catch (error) {
      
      res.status(500).json({
        message: error.message
      })

    }

  }

};

module.exports = UserController;
