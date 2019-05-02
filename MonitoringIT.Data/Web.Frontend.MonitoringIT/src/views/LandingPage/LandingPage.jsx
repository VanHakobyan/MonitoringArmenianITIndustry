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
import ProductSection from "./Sections/ProductSection.jsx";
import FavoriteProfiles from "./Sections/FavoriteProfiles.jsx";

import * as githubProfiles from "store/actions/githubProfiles"
import {
    favoriteGithubProfilesLoadingSelector,
    favoriteGithubProfilesSuccessSelector,
    favoriteGithubProfilesFailedSelector,
} from "store/selectors/githubProfiles";


import * as linkedinProfiles from "store/actions/linkedinProfiles"
import {
    favoriteLinkedinProfilesLoadingSelector,
    favoriteLinkedinProfilesSuccessSelector,
    favoriteLinkedinProfilesFailedSelector,
} from "store/selectors/linkedinProfiles";


const dashboardRoutes = [];
let count = 5;

class LandingPage extends React.Component {
    async componentDidMount() {
        await this.props.requestFavoriteGithubProfiles(6);
        await this.props.requestFavoriteLinkedinProfiles(6);
    }

    renderGithubProfiles = () => {
        let {favoriteGithubProfilesSuccess} = this.props;
        if (favoriteGithubProfilesSuccess) {
            return (
                <FavoriteProfiles
                    name="github"
                    title="People In Github"
                    profiles={favoriteGithubProfilesSuccess}
                    count={count}
                />
            )
        }
    };
    renderLinkedinProfiles = () => {
        let {favoriteLinkedinProfilesSuccess} = this.props;
        if (favoriteLinkedinProfilesSuccess) {
            return (
                <FavoriteProfiles
                    name="Linkedin"
                    title="People In Linkedin"
                    profiles={favoriteLinkedinProfilesSuccess}
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
                <Parallax filter image={require("assets/img/it.jpg")}>
                    <div className={classes.container}>
                        <GridContainer>
                            <GridItem xs={12} sm={12} md={6}>
                                <h1 className={classes.title}>Your Story Starts With Us.</h1>
                                <h4>
                                    Monitoring Armenian IT industry and platform for automatic job hiring
                                </h4>
                                <br/>
                                <Button
                                    color="danger"
                                    size="lg"
                                    href="https://youtu.be/pzwAvR3MxGE"
                                    target="_blank"
                                    rel="noopener noreferrer"
                                >
                                    <i className="fas fa-play"/>
                                    Watch video
                                </Button>
                            </GridItem>
                        </GridContainer>
                    </div>
                </Parallax>
                <div className={classNames(classes.main, classes.mainRaised)}>
                    <div className={classes.container}>
                        <ProductSection/>
                        {this.renderGithubProfiles()}
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
        favoriteGithubProfilesLoading: favoriteGithubProfilesLoadingSelector(state),
        favoriteGithubProfilesSuccess: favoriteGithubProfilesSuccessSelector(state),
        favoriteGithubProfilesFailed: favoriteGithubProfilesFailedSelector(state),

        favoriteLinkedinProfilesLoading: favoriteLinkedinProfilesLoadingSelector(state),
        favoriteLinkedinProfilesSuccess: favoriteLinkedinProfilesSuccessSelector(state),
        favoriteLinkedinProfilesFailed: favoriteLinkedinProfilesFailedSelector(state),
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
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(LandingPage));
