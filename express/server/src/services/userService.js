const User = require('../models/userModel');


const UserService = {

  createUser: async (userData) => {

    console.log('userData', userData);

    try {

      console.log('userData.email', userData.email);

      let user = await User.findOne({
        where: {
          email: userData.email
        }
      });
    
      if (user === null) {
        
        // Process data before saving

        user = await User.create(userData);
        return user;
    
      } else {
      
        console.log(`Email ${ userData.email } is already registered`);
      
      }
  
    } catch (error) {
            
      throw new Error(`Error creating user: ${ error.message }`);
  
    }

  },
  
  getUsers: async () => {

    try {
      
      const users = await User.findAll();
      return users;
  
    } catch (error) {
      
      throw new Error(`Error getting users: ${ error.message }`);
  
    }
    
  },

  getUser: async (id) => {

    try {
      
      const user = await User.findByPk(id);
      return user;

    } catch (error) {
      
      throw new Error(`Error getting user: ${ error.message }`);
    
    }
  
  },

  updateUser: async (id, userData) => {

    try {
      
      const user = await User.findByPk(id);
      await user.update(userData);
      return user;
    
    } catch (error) {

      throw new Error(`Error updating user: ${ error.message }`);
    
    }

  },

  deleteUser: async (id) => {

    try {
      
      const user = await User.findByPk(id);
      await user.destroy();

    } catch (error) {
      
      throw new Error(`Error deleting user: ${ error.message }`);

    }

  }

};

module.exports = UserService;
