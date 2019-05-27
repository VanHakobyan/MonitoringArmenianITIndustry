import React from "react";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import teamStyle from "assets/jss/material-kit-react/views/landingPageSections/teamStyle.jsx";
import Modal from "views/Modal";

class MainInfo extends React.Component {
	githubMainRenderer = () => {
		let {mainData} = this.props;
		return (
			<div className="main-info">
				<div className="img-container">
					<img className="profile-image" src={mainData.info.ImageUrl}/>
				</div>
				<div className="profile-info">
					<h2>{mainData.info.Name}</h2>
					<span className="lead">{mainData.info.Bio}</span>
					<div className="bio">{mainData.info.Company}</div>
					<div className="social">
						<Modal/>
					</div>
				</div>
			</div>
		)
	};
	linkedinMainRenderer = () => {
		let {mainData} = this.props;
		return (
			<div className="main-info">
				<div className="img-container">
					<img className="profile-image" src={mainData.info.ImageUrl}/>
				</div>
				<div className="profile-info">
					<h2>{mainData.info.FullName}</h2>
					<span className="lead">
						{
							mainData.info.Specialty ||
							(mainData.info.Education)
						}
					</span>
					<div className="bio">{mainData.info.Company}</div>
					<div className="social">
						<Modal/>
					</div>
				</div>
			</div>
		)
	};
	render() {
		let {mainData} = this.props;
		if(mainData.name === "github"){
			return (
				<>
					{this.githubMainRenderer()}
				</>
			)
		} else if(mainData.name === "linkedin") {
			return (
				<>
					{this.linkedinMainRenderer()}
				</>
			)
		}
	}
}

export default withStyles(teamStyle)(MainInfo);
