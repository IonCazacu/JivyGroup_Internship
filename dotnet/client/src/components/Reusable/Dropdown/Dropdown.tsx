import { Component } from 'react'

interface DropdownProps {
  options: {
    value: string;
    label: string;
  }[]
}

class Dropdown extends Component<DropdownProps> {
  
//   render() {
//     const { options } = this.props;

//     return (
//       <ul className="dropdown-menu">
//         <div className="dropdown-header">SUPPORTED LANGUAGES</div>
//         <hr />
//         { options.map((option, key) => (
//           <li
//             className="dropdown-item"
//             key={ key }>
//             <input
//               type="radio"
//               v-model="selectedLanguage"
//               key={ key }
//               value={ key } />
//             <label htmlFor={ key }>{ option }</label>
//           </li>
//         ))}
//       </ul>
//     )
//   }
}

export default Dropdown;
