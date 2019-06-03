import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";

// @material-ui/icons
// core components
import GridContainer from "components/Grid/GridContainer.jsx";
import JobCard from "views/LandingPage/Sections/JobCard";
import GithubCard from "views/LandingPage/Sections/GithubCard";
import LinkedinCard from "views/LandingPage/Sections/LinkedinCard";
import CompanyCard from "views/LandingPage/Sections/CompanyCard";

import teamStyle from "assets/jss/material-kit-react/views/landingPageSections/teamStyle.jsx";

class FavoriteProfiles extends React.Component {
	render() {
		let {profiles, count, title, name} = this.props;
		const {classes} = this.props;
		const imageClasses = classNames(
			classes.imgRaised,
			classes.imgRoundedCircle,
			classes.imgFluid
		);
		return (
			<div>
				<div className={classes.section}>
					<h2 className={classes.title}>{title}</h2>
					<div>
						<GridContainer>
							{
								profiles.map((item, key) => {
									if (name === "job") {
										return (
											<JobCard
												key={key}
												uniqueKey={key}
												item={item}
											/>
										)
									}
								})
							}
						</GridContainer>
						<GridContainer>
							{
								profiles.map((item, key) => {
									if (name === "github") {
										return (
											<GithubCard
												key={key}
												uniqueKey={key}
												item={item}
											/>
										)
									}
								})
							}
						</GridContainer>
						<GridContainer>
							{
								profiles.map((item, key) => {
									if (name === "linkedin") {
										return (
											<LinkedinCard
												key={key}
												uniqueKey={key}
												item={item}
											/>
										)
									}
								})
							}
						</GridContainer>
						<GridContainer>
							{
								profiles.map((item, key) => {
									if (name === "company") {
										return (
											<CompanyCard
												key={key}
												uniqueKey={key}
												item={item}
											/>
										)
									}
								})
							}
						</GridContainer>
					</div>
				</div>
			</div>
		);
	}
}

export default withStyles(teamStyle)(FavoriteProfiles);
