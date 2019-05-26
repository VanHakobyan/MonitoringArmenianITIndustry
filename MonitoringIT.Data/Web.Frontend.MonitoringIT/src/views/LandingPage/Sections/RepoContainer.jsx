import React from "react";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import RepoCard from "views/LandingPage/Sections/RepoCard";
import teamStyle from "assets/jss/material-kit-react/views/landingPageSections/teamStyle.jsx";

class RepoContainer extends React.Component {
	render() {
		let {repos} = this.props;
		return (
			<div className="section-wrapper">
				<div className="container-fluid">
					<div className="row">
						<div className="section-title">
							<h3>Repositories</h3>
						</div>
					</div>
					{
						repos.map((item, key) => {
							return (
								<RepoCard
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

export default withStyles(teamStyle)(RepoContainer);
