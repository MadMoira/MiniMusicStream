import React from 'react'
import ReactDOM from 'react-dom'
import App from './app/layout/App'
import './index.css'
import { store } from './app/stores/store';
import { Provider } from 'react-redux';

ReactDOM.render(
  <Provider store={store}>
    <React.StrictMode>
      <App />
    </React.StrictMode>
  </Provider>,
  document.getElementById('root')
)
