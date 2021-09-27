import React from 'react'
import { BrowserRouter as Router, Route, Switch, NavLink  } from 'react-router-dom'

import ArtistList from '../../components/ArtistList'
import './App.css'

function App() {
  return (
    <Router>
      <div className="App">
        <header className="app-head test items-center">
            <div className="order-first ml-2 test">Icon</div>
            <div className="menu-item test"><NavLink to="/artists">Artists</NavLink></div>
            <div className="menu-item test">Albums</div>
            <div className="menu-item test"><NavLink to="/songs">Songs</NavLink></div>
        </header>
        <section className="app-content test">
          <Switch> 
            <Route path="/artists">
              <ArtistList />
            </Route>
            <Route path="/songs">
              <h2>Songs</h2>
            </Route>
          </Switch>
        </section>
        <footer className="app-footer test">footer</footer>
      </div>
    </Router>
  )
}

export default App
