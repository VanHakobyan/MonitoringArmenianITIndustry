/*eslint-disable*/
import React from "react";
// react components for routing our app without refresh
import {Link} from "react-router-dom";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import headerLinksStyle from "assets/jss/material-kit-react/components/headerLinksStyle.jsx";
import headerList from "services/headerList";
let headerItemClass = "MuiButtonBase-root-89 MuiButton-root-63 MuiButton-text-65 MuiButton-flat-68 Header-title-11";

function NavigationBar({...props}) {
	let path = window.location.pathname.split("/")[1];
	return (
		<div className="navigation-w">
			{
				headerList.map((item, key) => {
					if(item.name.toLowerCase() === path.toLowerCase()){
						return (
							<Link
								key={key}
								to={item.path}
								className={headerItemClass + " active"}
							>
								{item.name}
							</Link>
						)
					} else {
						return (
							<Link
								key={key}
								to={item.path}
								className={headerItemClass}
							>
								{item.name}
							</Link>
						)
					}
				})
			}
		</div>
	);
}

export default withStyles(headerLinksStyle)(NavigationBar);