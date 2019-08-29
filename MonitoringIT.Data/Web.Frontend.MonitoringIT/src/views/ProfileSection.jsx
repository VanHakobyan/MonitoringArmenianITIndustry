import React from "react";
import classNames from "classnames";
import withStyles from "@material-ui/core/styles/withStyles";
import {connect} from "react-redux";

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
} from "store/selectors/profile";
import MainInfo from "views/LandingPage/Sections/MainInfo";
import DetailsCard from "views/LandingPage/Sections/DetailsCard";
import RepoContainer from "views/LandingPage/Sections/RepoContainer";
import SkillContainer from "views/LandingPage/Sections/SkillContainer";

class ProfileSection extends React.Component {
	async componentDidMount() {
		let args = window.location.pathname.split("/");
		await this.props.requestProfile(args[3], args[2]);
	}
	renderMainInfo = () => {
		let {currentProfileData} = this.props;
		if(currentProfileData && currentProfileData.main ) {
			return (
				<MainInfo
					mainData={currentProfileData.main}
				/>
			);
		}
	};
	renderDetails = () => {
		let {currentProfileData} = this.props;
		if(currentProfileData && currentProfileData.main ) {
			return (
				<DetailsCard
					mainData={currentProfileData.main}
				/>
			);
		}
	};
	renderRepos = () => {
		let {currentProfileData} = this.props;
		if(currentProfileData &&
			currentProfileData.main &&
			currentProfileData.main.info.GithubRepository &&
			currentProfileData.main.info.GithubRepository.length > 0
		){
			return (
				<RepoContainer
					repos={currentProfileData.main.info.GithubRepository}
				/>
			)
		}
	};
	renderSkills = () => {
		let {currentProfileData} = this.props;
		if(currentProfileData &&
			currentProfileData.main &&
			currentProfileData.main.info.LinkedinSkill &&
			currentProfileData.main.info.LinkedinSkill.length > 0
		){
			return (
				<SkillContainer
					skills={currentProfileData.main.info.LinkedinSkill}
				/>
			)
		}
	};
	render() {
		const { classes, ...rest } = this.props;
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
						{this.renderMainInfo()}
						<div className="details">
							<div className="section-wrapper">
								<div className="container-fluid">
									<div className="row">
										<div className="section-title">
											<h3>Details</h3>
										</div>
									</div>
									{this.renderDetails()}
								</div>
							</div>
							{this.renderRepos()}
							{this.renderSkills()}
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