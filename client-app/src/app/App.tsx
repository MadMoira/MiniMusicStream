import React from 'react'
import NavBar from './layout/NavBar'
import './App.css'

function App() {
  return (
    <div className="App">
      <NavBar />
      {/* <div className="app-menu test">menu</div> */}
      <div className="app-content test">content</div>
      <footer className="app-footer test">footer</footer>
    </div>
  )
}

export default App
