import React from "react";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import SkillCard from "views/LandingPage/Sections/SkillCard";
import teamStyle from "assets/jss/material-kit-react/views/landingPageSections/teamStyle.jsx";

class SkillContainer extends React.Component {
	render() {
		let {skills} = this.props;
		return (
			<div className="section-wrapper">
				<div className="container-fluid">
					<div className="row">
						<div className="section-title">
							<h3>Skills</h3>
						</div>
					</div>
					{
						skills.map((item, key) => {
							return (
								<SkillCard
									item={item}
									key={key}
									uniqueKey={key}
								/>
							)
						})
					}
				</div>
			</div>
		)
	}
}

export default withStyles(teamStyle)(SkillContainer);
