Feature: Low-stock alerts on the dashboard
  As a manager
  I want to see low-stock alerts on the dashboard
  So that I can prevent service interruptions

  Scenario: View ingredients below their reorder level
    Given the inventory contains an item named "Milk" with 2 units in stock and a reorder level of 5 units
    And the inventory contains an item named "Coffee Beans" with 10 units in stock and a reorder level of 5 units
    When the manager opens the dashboard
    Then the low-stock alerts show "Milk" with 2 units in stock and a reorder level of 5 units
    And the low-stock alerts do not show "Coffee Beans"
