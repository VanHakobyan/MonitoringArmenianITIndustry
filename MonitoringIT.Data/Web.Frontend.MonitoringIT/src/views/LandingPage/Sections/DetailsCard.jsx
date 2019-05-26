import React from "react";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";

import teamStyle from "assets/jss/material-kit-react/views/landingPageSections/teamStyle.jsx";

class DetailsCard extends React.Component {
	renderRow = (name, key) => {
		let {mainData} = this.props;
		if(mainData.info[key]){
			return (
				<address>
					<strong>{name}</strong>
					<br/>
					{mainData.info[key]}
				</address>
			);
		}
	};
	githubDetailsRenderer = () => {
		return (
			<div className="row">
				{this.renderRow("User Name", "UserName")}
				{this.renderRow("Company", "Company")}
				{this.renderRow("Email", "Email")}
				{this.renderRow("Blog or Website", "BlogOrWebsite")}
				{this.renderRow("Stars Count", "StarsCount")}
			</div>
		)
	};
	linkedinDetailsRenderer = () => {
		return (
			<div className="row">
				{this.renderRow("User Name", "FullName")}
				{this.renderRow("Education", "Education")}
				{this.renderRow("Company", "Company")}
				{this.renderRow("Email", "Email")}
				{this.renderRow("Connection Count", "ConnectionCount")}
			</div>
		)
	};
	render() {
		let {mainData} = this.props;
		if(mainData.name === "github"){
			return (
				<>
					{this.githubDetailsRenderer()}
				</>
			)
		}
		else if(mainData.name === "linkedin") {
			return (
				<>
					{this.linkedinDetailsRenderer()}
				</>
			)
		}
	}
}

export default withStyles(teamStyle)(DetailsCard);
