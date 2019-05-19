import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
import {connect} from "react-redux";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import FavoriteProfiles from "views/LandingPage/Sections/FavoriteProfiles.jsx";
// @material-ui/icons

// core components
import Header from "components/Header/Header.jsx";
import Footer from "components/Footer/Footer.jsx";
import HeaderLinks from "components/Header/HeaderLinks.jsx";
import NavigationBar from "components/Header/NavigationBar.jsx";
import Parallax from "components/Parallax/Parallax.jsx";

import landingPageStyle from "assets/jss/material-kit-react/views/landingPage.jsx";

// Sections for this page
import * as linkedinProfiles from "store/actions/linkedinProfiles"

import {
	byPageLinkedinProfilesLoadingSelector,
	byPageLinkedinProfilesSuccessSelector,
	byPageLinkedinProfilesFailedSelector,
} from "store/selectors/linkedinProfiles";


const dashboardRoutes = [];
let count = 5;

class LinkedinProfilesPage extends React.Component {
	async componentDidMount() {
		await this.props.requestLinkedinProfilesByPage(1, 12);
	}

	renderLinkedinProfiles = () => {
		let {byPageLinkedinProfilesSuccess} = this.props;
		if (byPageLinkedinProfilesSuccess) {
			return (
				<FavoriteProfiles
					name="linkedin"
					title="People In Linkedin"
					profiles={byPageLinkedinProfilesSuccess}
					count={count}
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
				<Parallax small filter image={require("assets/img/Custom/linkedin-b.jpg")}/>
				<div className={classNames(classes.main, classes.mainRaised)}>
					<div className={classes.container}>
						{this.renderLinkedinProfiles()}
					</div>
				</div>
				<Footer/>
			</div>
		);
	}
}


function mapStateToProps(state) {
	return {
		byPageLinkedinProfilesLoading: byPageLinkedinProfilesLoadingSelector(state),
		byPageLinkedinProfilesSuccess: byPageLinkedinProfilesSuccessSelector(state),
		byPageLinkedinProfilesFailed: byPageLinkedinProfilesFailedSelector(state),
	};
}

function mapDispatchToProps(dispatch) {
	return {
		requestLinkedinProfilesByPage: (currentPage,count) => {
		dispatch(linkedinProfiles.requestLinkedinProfilesByPage(currentPage,count))
		}
	};
}

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(LinkedinProfilesPage));