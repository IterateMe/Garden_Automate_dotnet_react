import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name;

    async actionValve(payload) {
        /*var payload = "turnOn1:"*/
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ command: payload })
        };
        //const requestOptions = this.set_post_request_options(payload)
        const response = await fetch('/valve', requestOptions);
        const data = await response.json();
    }

    render () {
    return (
        <div>
            <h1>Garden Automate</h1>
            <p>Please select your actions: </p>
            <button className="btn btn-primary" onClick={async () => { await this.actionValve("turnOn1:"); }}>ON</button>
            <button className="btn btn-primary" onClick={async () => { await this.actionValve("turnOff1:"); }}>OFF</button>
      </div>
    );
  }
}
