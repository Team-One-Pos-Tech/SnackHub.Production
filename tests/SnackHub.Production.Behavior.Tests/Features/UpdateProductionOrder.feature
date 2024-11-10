Feature: Update Production Order

Scenario: Update Status of Production Order
	Given existing a production order is in Received status
	When Update Status
	Then the Production Order should be in in Preparing status