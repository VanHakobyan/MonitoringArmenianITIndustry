import React from "react";
import {Provider} from "react-redux";
import configureStore from "store/index";
import ReactDOM from "react-dom";
import {createBrowserHistory} from "history";
import {Router, Route, Switch, Redirect} from "react-router-dom";
import "./assets/styles/index.scss";
import "assets/scss/index.scss";
import "assets/scss/material-kit-react.scss?v=1.4.0";

// pages for this product
import LandingPage from "views/LandingPage/LandingPage.jsx";
import GithubProfilesPage from "views/GithubProfilesPage.jsx";
import LinkedinProfilesPage from "views/LinkedinProfilesPage.jsx";
import ProfileSection from "views/ProfileSection.jsx";
import CompaniesPage from "views/CompaniesPage.jsx";
import JobsList from "views/JobsList.jsx";

let history = createBrowserHistory();
const store = configureStore();

ReactDOM.render(
	<Provider store={store}>
		<Router history={history}>
			<Switch>
				<Route exact path="/profile/:social/:id" component={ProfileSection}/>
				<Route path="/github" component={GithubProfilesPage}/>
				<Route path="/linkedin" component={LinkedinProfilesPage}/>
				<Route path="/companies" component={CompaniesPage}/>
				<Route path="/jobs" component={JobsList}/>
				<Route path="/" component={LandingPage}/>
				<Redirect to={{pathname: '/'}}/>
			</Switch>
		</Router>
	</Provider>,
	document.getElementById("root")
);
