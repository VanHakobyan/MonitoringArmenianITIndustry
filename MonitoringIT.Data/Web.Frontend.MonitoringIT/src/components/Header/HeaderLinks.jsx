/*eslint-disable*/
import React from "react";
// react components for routing our app without refresh
import {Link} from "react-router-dom";

// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import Tooltip from "@material-ui/core/Tooltip";

// @material-ui/icons
import {Apps, CloudDownload} from "@material-ui/icons";

// core components
import Button from "components/CustomButtons/Button.jsx";

import headerLinksStyle from "assets/jss/material-kit-react/components/headerLinksStyle.jsx";

function HeaderLinks({...props}) {
	const {classes} = props;
	return (
		<List className={classes.list}>
			<ListItem className={classes.listItem}>
				<Tooltip
					id="instagram-twitter"
					title="Project Github Link"
					placement={window.innerWidth > 959 ? "top" : "left"}
					classes={{tooltip: classes.tooltip}}
				>
					<Button
						href="https://github.com/VanHakobyan/MonitoringArmenianITIndustry"
						target="_blank"
						color="transparent"
						className={classes.navLink}
					>
						<i className={classes.socialIcons + " fab fa-github"}/>
					</Button>
				</Tooltip>
			</ListItem>
			<ListItem className={classes.listItem}>
				<Tooltip
					id="instagram-facebook"
					title="Follow me on Linkedin"
					placement={window.innerWidth > 959 ? "top" : "left"}
					classes={{tooltip: classes.tooltip}}
				>
					<Button
						color="transparent"
						href="https://www.linkedin.com/in/vanikhakobyan/"
						target="_blank"
						className={classes.navLink}
					>
						<i className={classes.socialIcons + " fab fa-linkedin"}/>
					</Button>
				</Tooltip>
			</ListItem>
			<ListItem className={classes.listItem}>
				<Tooltip
					id="instagram-facebook"
					title="Follow me on Medium"
					placement={window.innerWidth > 959 ? "top" : "left"}
					classes={{tooltip: classes.tooltip}}
				>
					<Button
						color="transparent"
						href="https://medium.com/@vanikhakobyan/"
						target="_blank"
						className={classes.navLink}
					>
						<i className={classes.socialIcons + " fab fa-medium"}/>
					</Button>
				</Tooltip>
			</ListItem>
		</List>
	);
}

export default withStyles(headerLinksStyle)(HeaderLinks);
