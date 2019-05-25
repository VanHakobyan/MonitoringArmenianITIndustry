import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
import {connect} from "react-redux";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import FavoriteProfiles from "views/LandingPage/Sections/ProfilesList.jsx";
// @material-ui/icons

// core components
import Header from "components/Header/Header.jsx";
import Footer from "components/Footer/Footer.jsx";
import HeaderLinks from "components/Header/HeaderLinks.jsx";
import NavigationBar from "components/Header/NavigationBar.jsx";
import Parallax from "components/Parallax/Parallax.jsx";

import landingPageStyle from "assets/jss/material-kit-react/views/landingPage.jsx";

import * as jobs from "store/actions/jobs";
// Sections for this page
import {
	jobsLoadingSelector,
	jobsSuccessSelector,
	jobsFailedSelector,
} from "store/selectors/jobs";
const dashboardRoutes = [];

class JobsList extends React.Component {
	async componentDidMount() {
		await this.props.requestJobs(12);
	}
	componentWillUnmount() {
		window.scrollTo(0, 0);
	}
	renderJobs = () => {
		let {jobsSuccess} = this.props;
		if (jobsSuccess) {
			return (
				<FavoriteProfiles
					name="job"
					title="Jobs"
					requestJobs={this.props.requestJobs}
					profiles={jobsSuccess}
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
				<Parallax small filter image={require("assets/img/Custom/jobs-b.jpeg")}/>
				<div className={classNames(classes.main, classes.mainRaised)}>
					<div className={classes.container}>
						{this.renderJobs()}
					</div>
				</div>
				<Footer/>
			</div>
		);
	}
}


function mapStateToProps(state) {
	return {
		jobsLoading: jobsLoadingSelector(state),
		jobsSuccess: jobsSuccessSelector(state),
		jobsFailed: jobsFailedSelector(state),
	};
}

function mapDispatchToProps(dispatch) {
	return {
		requestJobs: count => {
			dispatch(jobs.requestJobs(count))
		}
	};
}

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(JobsList));