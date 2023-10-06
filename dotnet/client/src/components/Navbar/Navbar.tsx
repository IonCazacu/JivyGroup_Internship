import { Component } from 'react';
import { Link } from 'react-router-dom';
import logo from './logo192.png';

import './Navbar.scss';

// interface NavbarState {
//   options: {
//     [key: string]: string;
//   }
// }

class Navbar extends Component {

  toggleSidebarNav($event: any) {
    console.log($event);
  }

  toggleDropdownMenu($event: any) {
    console.log($event);
  }

  render() {
    return (
      <nav className="navbar">
        <div className="container">
          <div className="navbar-left">
            <Link className="navbar-brand" to="/">
              <img src={ logo } alt="logo" />
            </Link>
            <button
              className="navbar-toggler"
              type="button"
              aria-controls=""
              aria-expanded="false"
              aria-label="Toggle navigation"
              onClick={ this.toggleSidebarNav }>
              <i className="bi bi-list"></i>
              {/* <span v-if="!isMobile" className="toggler-text">Menu</span> */}
            </button>
          </div>
          
          <div className="navbar-right">
            <ul className="navbar-nav">
              <li className="nav-item">
                <Link
                  className="nav-link"
                  to="/signup">
                  Sign Up
                </Link>
                <Link
                  className="nav-link"
                  to="/users">
                  Get Users
                </Link>
              </li>
              <li className="nav-item dropdown">
                <button
                  type="button"
                  aria-controls=""
                  aria-expanded="false"
                  className="nav-link dropdown-toggler"
                  onClick={ this.toggleDropdownMenu }>
                  {/* :class="{ 'expanded' : isDropdownMenuOpen }" */}
                  <span className="toggler-text">EN</span>
                </button>
                {/* <dropdown-menu v-if="isDropdownMenuOpen"></dropdown-menu> */}
              </li>
            </ul>
          </div>
          
          {/* <transition name="sidebar-transitions">
            <div v-if="isMobileSidebarNavOpen" class="sidebar">
              <div class="sidebar-top">
                <button
                  class="navbar-toggler"
                  type="button"
                  aria-controls=""
                  aria-expanded="false"
                  aria-label="Toggle navigation"
                  @click.stop="toggleSidebarNav">
                  <i class="bi bi-x"></i>
                </button>
              </div>
              <ul class="sidebar-nav">
                <li class="nav-item">
                  <router-link
                    class="nav-link"
                    :to="{ name : '' }">
                    Sign In
                  </router-link>
                </li>
              </ul>
            </div>
          </transition> */}

        </div>
      </nav>
    )
  }
}

export default Navbar;
