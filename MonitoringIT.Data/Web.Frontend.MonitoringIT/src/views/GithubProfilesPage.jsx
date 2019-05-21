import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
import {connect} from "react-redux";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";

// @material-ui/icons

// core components
import Header from "components/Header/Header.jsx";
import Footer from "components/Footer/Footer.jsx";
import HeaderLinks from "components/Header/HeaderLinks.jsx";
import NavigationBar from "components/Header/NavigationBar.jsx";
import Parallax from "components/Parallax/Parallax.jsx";

import FavoriteProfiles from "views/LandingPage/Sections/ProfilesList.jsx";

import landingPageStyle from "assets/jss/material-kit-react/views/landingPage.jsx";

// Sections for this page
import * as githubProfiles from "store/actions/githubProfiles";
import {
	allProfilesSuccessSelector,
	allProfilesLoadingSelector,
	allProfilesFailedSelector,
} from "store/selectors/githubProfiles";

const dashboardRoutes = [];

class GithubProfilesPage extends React.Component {
	async componentDidMount() {
		await this.props.requestAllGithubProfiles(12);
	}
	renderGithubProfiles = () => {
		let {allProfilesSuccess} = this.props;
		if (allProfilesSuccess) {
			return (
				<FavoriteProfiles
					name="github"
					title="People In Github"
					requestAllGithubProfiles={this.props.requestAllGithubProfiles}
					profiles={allProfilesSuccess}
				/>
			)
		}
	};
	render() {
		const {classes, ...rest} = this.props;
		return (
			<div>
				<Header
					color="transparent"
					routes={dashboardRoutes}
					brand="Monitoring IT"
					rightLinks={<HeaderLinks/>}
					leftLinks={<NavigationBar/>}
					fixed
					changeColorOnScroll={{
						height: 400,
						color: "white"
					}}
					{...rest}
				/>
				<Parallax small filter image={require("assets/img/Custom/github-b.jpg")}/>
				<div className={classNames(classes.main, classes.mainRaised)}>
					<div className={classes.container}>
						{this.renderGithubProfiles()}
					</div>
				</div>
				<Footer/>
			</div>
		);
	}
}


function mapStateToProps(state) {
	return {
		allProfilesSuccess: allProfilesSuccessSelector(state),
		allProfilesLoading: allProfilesLoadingSelector(state),
		allProfilesFailed: allProfilesFailedSelector(state),
	};
}

function mapDispatchToProps(dispatch) {
	return {
		requestAllGithubProfiles: count => {
			dispatch(githubProfiles.requestAllGithubProfiles(count))
		},
	};
}

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(GithubProfilesPage));