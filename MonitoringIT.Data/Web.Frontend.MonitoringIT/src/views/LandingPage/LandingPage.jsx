import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
import { connect } from "react-redux";
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
import Parallax from "components/Parallax/Parallax.jsx";

import landingPageStyle from "assets/jss/material-kit-react/views/landingPage.jsx";

// Sections for this page
import ProductSection from "./Sections/ProductSection.jsx";
import TeamSection from "./Sections/TeamSection.jsx";
import WorkSection from "./Sections/WorkSection.jsx";

import * as githubProfiles from "store/actions/githubProfiles"
import {
	allProfilesLoadingSelector,
	allProfilesSuccessSelector,
	allProfilesFailedSelector,
} from "store/selectors/githubProfiles";

const dashboardRoutes = [];

class LandingPage extends React.Component {
	async componentDidMount() {
		await this.props.requestAllGithubProfiles();
	}
  render() {
    const { classes, ...rest } = this.props;
    return (
      <div>
        <Header
          color="transparent"
          routes={dashboardRoutes}
          brand="Monitoring IT"
          rightLinks={<HeaderLinks />}
          fixed
          changeColorOnScroll={{
            height: 400,
            color: "white"
          }}
          {...rest}
        />
        <Parallax filter image={require("assets/img/it.jpg")}>
          <div className={classes.container}>
            <GridContainer>
              <GridItem xs={12} sm={12} md={6}>
                <h1 className={classes.title}>Your Story Starts With Us.</h1>
                <h4>
                    Monitoring Armenian IT industry and platform for automatic job hiring
                </h4>
                <br />
                <Button
                  color="danger"
                  size="lg"
                  href="https://youtu.be/pzwAvR3MxGE"
                  target="_blank"
                  rel="noopener noreferrer"
                >
                  <i className="fas fa-play" />
                  Watch video
                </Button>
              </GridItem>
            </GridContainer>
          </div>
        </Parallax>
        <div className={classNames(classes.main, classes.mainRaised)}>
          <div className={classes.container}>
            <ProductSection />
						<TeamSection />
            <WorkSection />
          </div>
        </div>
        <Footer />
      </div>
    );
  }
}


function mapStateToProps(state) {
	return {
		allProfilesLoading: allProfilesLoadingSelector(state),
		allProfilesSuccess: allProfilesSuccessSelector(state),
		allProfilesFailed: allProfilesFailedSelector(state),
	};
}

function mapDispatchToProps(dispatch) {
	return {
		requestAllGithubProfiles: () => {
			dispatch(githubProfiles.requestAllGithubProfiles())
		},
	};
}
export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(LandingPage));
