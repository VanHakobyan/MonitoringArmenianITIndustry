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
import GridContainer from "components/Grid/GridContainer.jsx";
import GridItem from "components/Grid/GridItem.jsx";
import Button from "components/CustomButtons/Button.jsx";
import HeaderLinks from "components/Header/HeaderLinks.jsx";
import NavigationBar from "components/Header/NavigationBar.jsx";
import Parallax from "components/Parallax/Parallax.jsx";

import landingPageStyle from "assets/jss/material-kit-react/views/landingPage.jsx";

// Sections for this page
import * as githubProfiles from "store/actions/githubProfiles";
import * as linkedinProfiles from "store/actions/linkedinProfiles"
import * as companies from "store/actions/companies";
import {
	favoriteGithubProfilesLoadingSelector,
	favoriteGithubProfilesSuccessSelector,
	favoriteGithubProfilesFailedSelector,
} from "store/selectors/githubProfiles";
import {
	favoriteLinkedinProfilesLoadingSelector,
	favoriteLinkedinProfilesSuccessSelector,
	favoriteLinkedinProfilesFailedSelector,
} from "store/selectors/linkedinProfiles";
import {
	favoriteCompaniesLoadingSelector,
	favoriteCompaniesSuccessSelector,
	favoriteCompaniesFailedSelector,
} from "store/selectors/companies";


const dashboardRoutes = [];
let count = 5;

class LinkedinProfilesPage extends React.Component {
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
				<Parallax small filter image={require("assets/img/Custom/linkedin-b.jpg")}/>
				<Footer/>
			</div>
		);
	}
}


function mapStateToProps(state) {
	return {
		favoriteGithubProfilesLoading: favoriteGithubProfilesLoadingSelector(state),
		favoriteGithubProfilesSuccess: favoriteGithubProfilesSuccessSelector(state),
		favoriteGithubProfilesFailed: favoriteGithubProfilesFailedSelector(state),

		favoriteLinkedinProfilesLoading: favoriteLinkedinProfilesLoadingSelector(state),
		favoriteLinkedinProfilesSuccess: favoriteLinkedinProfilesSuccessSelector(state),
		favoriteLinkedinProfilesFailed: favoriteLinkedinProfilesFailedSelector(state),

		favoriteCompaniesLoading: favoriteCompaniesLoadingSelector(state),
		favoriteCompaniesSuccess: favoriteCompaniesSuccessSelector(state),
		favoriteCompaniesFailed: favoriteCompaniesFailedSelector(state),
	};
}

function mapDispatchToProps(dispatch) {
	return {
		requestFavoriteGithubProfiles: count => {
			dispatch(githubProfiles.requestFavoriteGithubProfiles(count))
		},
		requestFavoriteLinkedinProfiles: count => {
			dispatch(linkedinProfiles.requestFavoriteLinkedinProfiles(count))
		},
		requestFavoriteCompanies: count => {
			dispatch(companies.requestFavoriteCompanies(count))
		},
	};
}

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(LinkedinProfilesPage));