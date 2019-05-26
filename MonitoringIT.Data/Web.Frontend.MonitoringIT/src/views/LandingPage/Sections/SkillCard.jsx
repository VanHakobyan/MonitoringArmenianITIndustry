import React from "react";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import teamStyle from "assets/jss/material-kit-react/views/landingPageSections/teamStyle.jsx";

class SkillCard extends React.Component {
	render() {
		let {item, uniqueKey} = this.props;
		return (
			<address key={uniqueKey}>
				<strong>{item.Name}</strong>
				<br/>
				{`${item.EndorsedCount}`}
			</address>
		)
	}
}

export default withStyles(teamStyle)(SkillCard);
