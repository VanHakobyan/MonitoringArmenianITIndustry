import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
import InfiniteScroll from "react-infinite-scroller";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";

// @material-ui/icons
// core components
import GridContainer from "components/Grid/GridContainer.jsx";
import GridItem from "components/Grid/GridItem.jsx";
import Button from "components/CustomButtons/Button.jsx";
import Card from "components/Card/Card.jsx";
import CardBody from "components/Card/CardBody.jsx";
import CardFooter from "components/Card/CardFooter.jsx";
import JobCard from "views/LandingPage/Sections/JobCard";
import GithubCard from "views/LandingPage/Sections/GithubCard";
import LinkedinCard from "views/LandingPage/Sections/LinkedinCard";
import CompanyCard from "views/LandingPage/Sections/CompanyCard";

import teamStyle from "assets/jss/material-kit-react/views/landingPageSections/teamStyle.jsx";

class ProfilesList extends React.Component {
	renderInfiniteScroll = () => {
		let {name, profiles} = this.props;
		let items = [];
		const loader = <div className="loader">Loading ...</div>;
		if(name === "github") {
			profiles.map((item, key) => {
				items.push(
					<GithubCard key={key} uniqueKey={key} item={item}/>
				)
			});
			return (
				<InfiniteScroll
					pageStart={0}
					loadMore={() => this.props.requestAllGithubProfiles(12)}
					hasMore={true}
					loader={loader}>
					<GridContainer>
						{items}
					</GridContainer>
				</InfiniteScroll>
			)
		} else if(name === "linkedin") {
			profiles.map((item, key) => {
				items.push(
					<LinkedinCard key={key} uniqueKey={key} item={item}/>
				)
			});
			return (
				<InfiniteScroll
					pageStart={0}
					loadMore={() => this.props.requestAllLinkedinProfiles(12)}
					hasMore={true}
					loader={loader}>
					<GridContainer>
						{items}
					</GridContainer>
				</InfiniteScroll>
			)
		} else if(name === "job") {
			profiles.map((item, key) => {
				items.push(
					<JobCard key={key} uniqueKey={key} item={item}/>
				)
			});
			return (
				<InfiniteScroll
					pageStart={0}
					loadMore={() => this.props.requestJobs(12)}
					hasMore={true}
					loader={loader}>
					<GridContainer>
						{items}
					</GridContainer>
				</InfiniteScroll>
			)
		} else if(name === "company") {
			profiles.map((item, key) => {
				items.push(
					<CompanyCard key={key} uniqueKey={key} item={item}/>
				)
			});
			return (
				<InfiniteScroll
					pageStart={0}
					loadMore={() => this.props.requestCompanies(12)}
					hasMore={true}
					loader={loader}>
					<GridContainer>
						{items}
					</GridContainer>
				</InfiniteScroll>
			)
		}
	};
	render() {
		let {profiles, title, name} = this.props;
		const { classes } = this.props;
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
						{this.renderInfiniteScroll()}
						<GridContainer>
							{
								profiles.map((item, key) => {
									if(name === "company"){
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

export default withStyles(teamStyle)(ProfilesList);
