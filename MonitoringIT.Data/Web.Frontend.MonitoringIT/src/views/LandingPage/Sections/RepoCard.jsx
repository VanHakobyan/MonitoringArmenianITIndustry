import React from "react";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import teamStyle from "assets/jss/material-kit-react/views/landingPageSections/teamStyle.jsx";

class RepoCard extends React.Component {
	render() {
		let {item, uniqueKey} = this.props;
		return (
			<address key={uniqueKey}>
				<strong>{item.Name}</strong>
				{
					item.GithubLanguage.map((el, k) => {
						return (
							<>
								<br/>
								{`${el.Name} \u00A0\u00A0 ${el.Percent} %`}
							</>
						)
					})
				}
				<br/>
			</address>
		)
	}
}

export default withStyles(teamStyle)(RepoCard);
