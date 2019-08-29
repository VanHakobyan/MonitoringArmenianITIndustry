import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
import {connect} from "react-redux";
import withStyles from "@material-ui/core/styles/withStyles";
import ProfilesList from "views/LandingPage/Sections/ProfilesList.jsx";
// @material-ui/icons

// core components
import Header from "components/Header/Header.jsx";
import Footer from "components/Footer/Footer.jsx";
import HeaderLinks from "components/Header/HeaderLinks.jsx";
import NavigationBar from "components/Header/NavigationBar.jsx";
import Parallax from "components/Parallax/Parallax.jsx";

import landingPageStyle from "assets/jss/material-kit-react/views/landingPage.jsx";

// Sections for this page
import * as companies from "store/actions/companies";

import {
	companiesLoadingSelector,
	companiesSuccessSelector,
	companiesFailedSelector,
} from "store/selectors/companies";

const dashboardRoutes = [];

class CompaniesPage extends React.Component {
	async componentDidMount() {
		await this.props.requestCompanies(12);
	}
	componentWillUnmount() {
		window.scrollTo(0, 0);
	}
	renderCompanies = () => {
		let {companiesSuccess} = this.props;
		if (companiesSuccess) {
			return (
				<ProfilesList
					name="company"
					title="Companies"
					requestCompanies={this.props.requestCompanies}
					profiles={companiesSuccess}
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
				<Parallax small filter image={require("assets/img/Custom/companies-b.jpg")}/>
				<div className={classNames(classes.main, classes.mainRaised)}>
					<div className={classes.container}>
						{this.renderCompanies()}
					</div>
				</div>
				<Footer/>
			</div>
		);
	}
}


function mapStateToProps(state) {
	return {
		companiesLoading: companiesLoadingSelector(state),
		companiesSuccess: companiesSuccessSelector(state),
		companiesFailed: companiesFailedSelector(state),
	};
}

function mapDispatchToProps(dispatch) {
	return {
		requestCompanies: count => {
			dispatch(companies.requestCompanies(count))
		}
	};
}

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(CompaniesPage));