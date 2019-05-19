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

import * as jobs from "store/actions/jobs";

// Sections for this page
import {
	byPageJobsLoadingSelector,
	byPageJobsSuccessSelector,
	byPageJobsFailedSelector,
} from "store/selectors/jobs";


const dashboardRoutes = [];
let count = 5;

class JobsList extends React.Component {
	async componentDidMount() {
		await this.props.requestJobsByPage(1, 12);
	}

	renderJobs = () => {
		let {byPageJobsSuccess} = this.props;
		if (byPageJobsSuccess) {
			return (
				<FavoriteProfiles
					name="job"
					title="Jobs"
					profiles={byPageJobsSuccess}
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
		byPageJobsLoading: byPageJobsLoadingSelector(state),
		byPageJobsSuccess: byPageJobsSuccessSelector(state),
		byPageJobsFailed: byPageJobsFailedSelector(state),
	};
}

function mapDispatchToProps(dispatch) {
	return {
		requestJobsByPage: (currentPage, count) => {
			dispatch(jobs.requestJobsByPage(currentPage, count))
		}
	};
}

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(JobsList));