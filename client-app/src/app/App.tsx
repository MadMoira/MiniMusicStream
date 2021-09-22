import React from 'react'
import NavBar from './layout/NavBar'
import './App.css'

function App() {
  return (
    <div className="App">
      <header className="app-head test items-center">
          <div className="order-first ml-2 test">Icon</div>
          <div className="menu-item test">Artists</div>
          <div className="menu-item test">Albums</div>
          <div className="menu-item test">Songs</div>
      </header>
      <section className="app-content test">
        <div className="flex flex-wrap">
          <div className="card test">
            <div className="logo test" />
            <div className="test">Artist name</div>
          </div>
          <div className="card test">
            <div className="logo test" />
            <div className="test">Artist name</div>
          </div>
          <div className="card test">
            <div className="logo test" />
            <div className="test">Artist name</div>
          </div>
          <div className="card test">
            <div className="logo test" />
            <div className="test">Artist name</div>
          </div>
        </div>
      </section>
      <footer className="app-footer test">footer</footer>
    </div>
  )
}

export default App
