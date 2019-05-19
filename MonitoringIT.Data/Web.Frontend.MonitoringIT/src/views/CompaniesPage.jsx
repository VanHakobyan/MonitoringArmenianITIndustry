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
import * as companies from "store/actions/companies";

import {
	byPageCompaniesLoadingSelector,
	byPageCompaniesSuccessSelector,
	byPageCompaniesFailedSelector,
} from "store/selectors/companies";


const dashboardRoutes = [];
let count = 5;

class CompaniesPage extends React.Component {

    async componentDidMount() {
        await this.props.requestCompaniesByPage(1, 12);
    }

    renderCompanies = () => {
        let {byPageCompaniesSuccess} = this.props;
        if (byPageCompaniesSuccess) {
            return (
                <FavoriteProfiles
                    name="company"
                    title="Companies"
                    profiles={byPageCompaniesSuccess}
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
		byPageCompaniesLoading: byPageCompaniesLoadingSelector(state),
		byPageCompaniesSuccess: byPageCompaniesSuccessSelector(state),
		byPageCompaniesFailed: byPageCompaniesFailedSelector(state),
	};
}

function mapDispatchToProps(dispatch) {
	return {
		requestCompaniesByPage:  (currentPage,count) => {
			dispatch(companies.requestCompaniesByPage (currentPage,count))
		}
	};
}

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(CompaniesPage));