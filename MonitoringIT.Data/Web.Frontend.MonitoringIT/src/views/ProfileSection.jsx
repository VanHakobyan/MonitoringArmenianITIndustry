import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import {connect} from "react-redux";
// @material-ui/icons
// core components
import Header from "components/Header/Header.jsx";
import Footer from "components/Footer/Footer.jsx";
import HeaderLinks from "components/Header/HeaderLinks.jsx";
import NavigationBar from "components/Header/NavigationBar.jsx";
import Parallax from "components/Parallax/Parallax.jsx";
import landingPageStyle from "assets/jss/material-kit-react/views/landingPage.jsx";

import * as profile from "store/actions/profile";
import {
	currentProfileLoadingSelector,
	currentProfileDataSelector,
	currentProfileFailedSelector
} from "store/selectors/profile"

class ProfileSection extends React.Component {
	async componentDidMount() {
		let args = window.location.pathname.split("/");
		await this.props.requestProfile(args[3], args[2]);
	}
	render() {
		const { classes, ...rest } = this.props;
		const imageClasses = classNames(
			classes.imgRaised,
			classes.imgRoundedCircle,
			classes.imgFluid
		);
		return (
			<div>
				<Header
					color="transparent"
					brand="Monitoring IT"
					rightLinks={<HeaderLinks />}
					leftLinks={<NavigationBar/>}
					fixed
					changeColorOnScroll={{
						height: 200,
						color: "white"
					}}
					{...rest}
				/>
				<Parallax small filter image={require("assets/img/profile-bg.jpg")} />
				<div className={classNames(classes.main, classes.mainRaised)}>
					<div className="profile-container">
						<div className="main-info">
							<div className="img-container">
								<img className="profile-image" src={require("assets/img/profile-bg.jpg")}/>
							</div>
							<div className="profile-info">
								<h2>Anun Azganun</h2>
								<span className="lead">Web Developer</span>
								<div className="bio">
									Credibly embrace visionary internal or "organic" sources and business benefits. Collaboratively integrate efficient portals rather than customized customer service. Assertively deliver frictionless services via leveraged interfaces. Conveniently evisculate accurate sources and process-centric expertise.

									Energistically fabricate customized imperatives through cooperative catalysts for change.
								</div>
								<div className="social">
									<HeaderLinks/>
								</div>
							</div>
						</div>
						<div className="details">

						</div>
					</div>
				</div>
				<Footer />
			</div>
		);
	}
}

function mapStateToProps(state) {
	return {
		currentProfileLoading: currentProfileLoadingSelector(state),
		currentProfileData: currentProfileDataSelector(state),
		currentProfileFailed: currentProfileFailedSelector(state)
	};
}

function mapDispatchToProps(dispatch) {
	return {
		requestProfile: (id, social) => {
			dispatch(profile.requestProfile(id, social))
		}
	};
}

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(landingPageStyle)(ProfileSection));