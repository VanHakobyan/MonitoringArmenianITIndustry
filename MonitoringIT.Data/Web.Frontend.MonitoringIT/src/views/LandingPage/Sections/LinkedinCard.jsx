import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import {Link} from "react-router-dom";

// @material-ui/icons
// core components
import GridItem from "components/Grid/GridItem.jsx";
import Button from "components/CustomButtons/Button.jsx";
import Card from "components/Card/Card.jsx";
import CardBody from "components/Card/CardBody.jsx";
import CardFooter from "components/Card/CardFooter.jsx";

import teamStyle from "assets/jss/material-kit-react/views/landingPageSections/teamStyle.jsx";

class LinkedinCard extends React.Component {
	render() {
		let {item, uniqueKey} = this.props;
		const {classes} = this.props;
		const imageClasses = classNames(
			classes.imgRaised,
			classes.imgRoundedCircle,
			classes.imgFluid
		);
		return (
			<GridItem key={uniqueKey} xs={12} sm={12} md={4}>
				<Card plain>
					<Link to={`/profile/linkedin/${item.Id}`}>
						<GridItem xs={12} sm={12} md={6} className={classes.itemGrid}>
							<img src={item.ImageUrl} alt="..." className={imageClasses}/>
						</GridItem>
						<h4 className={classes.cardTitle}>
							{item.FullName}
							<br/>
							<small className={classes.smallTitle}>{item.Company}</small>
						</h4>
						<CardBody>
							<p className={classes.description + " lines-l"}>
								{item.Specialty}
							</p>
						</CardBody>
					</Link>
					<CardFooter className={classes.justifyCenter}>
						<Button
							justIcon
							color="transparent"
							className={classes.margin5}
							href={`https://www.linkedin.com/in/${item.Username}`}
							target="_blank"
						>
							<i className={classes.socials + " fab fa-linkedin"}/>
						</Button>
					</CardFooter>
				</Card>
			</GridItem>
		);
	}
}

export default withStyles(teamStyle)(LinkedinCard);
