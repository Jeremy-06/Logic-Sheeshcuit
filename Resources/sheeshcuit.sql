-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 04, 2025 at 02:52 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sheeshcuit`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin_users`
--

CREATE TABLE `admin_users` (
  `adminId` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `adminRole` enum('super_admin','inventory_manager','order_manager','financial_auditor') NOT NULL,
  `fullName` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `isActive` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `admin_users`
--

INSERT INTO `admin_users` (`adminId`, `userId`, `adminRole`, `fullName`, `email`, `phone`, `isActive`) VALUES
(1, 2, 'super_admin', 'Julianne Tumpap', 'queenlilith@gmail.com', '09771910453', 1),
(2, 3, 'super_admin', 'Primavera Ron Jeremy', 'primaveraron@gmail.com', '9943327537', 1),
(6, 7, 'inventory_manager', 'Robert Downey', 'cuterobert@gmail.com', '111222333', 1),
(7, 8, 'financial_auditor', 'Twilight Sparkle', 'twilight@gmail.com', '09123456781', 1),
(8, 10, 'order_manager', 'Pinkie Pie', 'pinkie@gmail.com', '9128381781', 1);

-- --------------------------------------------------------

--
-- Table structure for table `cart`
--

CREATE TABLE `cart` (
  `cartId` int(11) NOT NULL,
  `products_productId` int(11) NOT NULL,
  `customers_customerId` int(11) NOT NULL,
  `productQty` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `cart`
--

INSERT INTO `cart` (`cartId`, `products_productId`, `customers_customerId`, `productQty`) VALUES
(42, 7, 1, 3),
(53, 9, 1, 3),
(57, 21, 1, 5);

-- --------------------------------------------------------

--
-- Table structure for table `customers`
--

CREATE TABLE `customers` (
  `customerId` int(11) NOT NULL,
  `customerFname` varchar(45) NOT NULL,
  `customerLname` varchar(45) NOT NULL,
  `customerAddress` varchar(45) NOT NULL,
  `customerPhone` varchar(20) DEFAULT NULL,
  `userId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `customers`
--

INSERT INTO `customers` (`customerId`, `customerFname`, `customerLname`, `customerAddress`, `customerPhone`, `userId`) VALUES
(1, 'Jeremy', 'Primavera', 'PH', '143', 1),
(2, 'Julianne', 'Tumpap', 'Paranaque', '09771910453', 2),
(3, 'Donn', 'Torres', 'Taguig', '9693475278', 9);

-- --------------------------------------------------------

--
-- Table structure for table `expenses`
--

CREATE TABLE `expenses` (
  `expensesId` int(11) NOT NULL,
  `expenseDate` date NOT NULL,
  `expenseDescription` varchar(255) NOT NULL,
  `expenseAmount` decimal(10,2) NOT NULL,
  `expenseCategory` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `expenses`
--

INSERT INTO `expenses` (`expensesId`, `expenseDate`, `expenseDescription`, `expenseAmount`, `expenseCategory`) VALUES
(1, '2025-07-31', 'pambili ulam, hotdog', 100.00, 'Other'),
(2, '2025-08-03', 'pambili tinapay', 200.00, 'Other'),
(3, '2025-08-03', 'bayad kuryente', 500.00, 'Utilities'),
(4, '2025-08-04', 'binulsa ni admin', 10.00, 'Travel');

-- --------------------------------------------------------

--
-- Table structure for table `inventory`
--

CREATE TABLE `inventory` (
  `inventoryId` int(11) NOT NULL,
  `productStock` int(11) DEFAULT NULL,
  `products_productId` int(11) NOT NULL,
  `suppliers_supplierId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `inventory`
--

INSERT INTO `inventory` (`inventoryId`, `productStock`, `products_productId`, `suppliers_supplierId`) VALUES
(1, 44, 1, 1),
(2, 90, 2, 1),
(3, 116, 3, 1),
(4, 90, 4, 4),
(5, 85, 5, 4),
(6, 110, 6, 4),
(7, 149, 7, 1),
(8, 130, 8, 4),
(9, 178, 9, 1),
(10, 155, 10, 2),
(11, 140, 11, 2),
(12, 198, 12, 1),
(13, 200, 13, 2),
(14, 95, 14, 2),
(15, 99, 15, 2),
(16, 97, 16, 2),
(17, 97, 17, 2),
(18, 97, 18, 2),
(19, 100, 19, 2),
(20, 100, 20, 2),
(21, 176, 21, 2),
(22, 297, 22, 2),
(23, 280, 23, 2),
(24, 260, 24, 2),
(25, 237, 25, 2),
(26, 217, 26, 2),
(27, 197, 27, 2),
(28, 100, 28, 2),
(29, 150, 29, 2),
(30, 130, 30, 2),
(31, 119, 31, 2),
(32, 87, 32, 2),
(33, 77, 33, 2),
(34, 67, 34, 2),
(35, 1, 35, 3),
(36, 1, 36, 3),
(37, 92, 37, 2);

-- --------------------------------------------------------

--
-- Table structure for table `orderitems`
--

CREATE TABLE `orderitems` (
  `orderItemsId` int(11) NOT NULL,
  `productQty` int(11) NOT NULL,
  `orders_orderId` int(11) NOT NULL,
  `products_productId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `orderitems`
--

INSERT INTO `orderitems` (`orderItemsId`, `productQty`, `orders_orderId`, `products_productId`) VALUES
(2, 1, 3, 2),
(3, 1, 28, 12),
(4, 1, 28, 3),
(5, 2, 23, 9),
(6, 1, 14, 7),
(7, 3, 11, 21),
(8, 2, 10, 1),
(9, 3, 13, 16),
(10, 1, 17, 36),
(11, 1, 17, 31),
(12, 1, 19, 35),
(13, 1, 28, 15),
(14, 3, 22, 18),
(15, 1, 28, 21),
(16, 3, 28, 14),
(17, 3, 28, 37),
(18, 3, 28, 17),
(19, 3, 32, 32),
(20, 3, 32, 33),
(21, 3, 32, 34),
(22, 3, 37, 27),
(23, 3, 37, 25),
(24, 3, 37, 26),
(25, 3, 37, 22),
(28, 3, 47, 3),
(30, 6, 43, 1),
(32, 1, 47, 12),
(33, 5, 47, 2),
(34, 1, 49, 14),
(35, 1, 51, 14),
(38, 5, 55, 10);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `orderId` int(11) NOT NULL,
  `orderDate` date NOT NULL,
  `orderStatus` varchar(45) NOT NULL,
  `customers_customerId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`orderId`, `orderDate`, `orderStatus`, `customers_customerId`) VALUES
(3, '2025-07-27', 'completed', 1),
(10, '2025-07-27', 'completed', 1),
(11, '2025-07-27', 'completed', 1),
(13, '2025-07-27', 'completed', 1),
(14, '2025-07-27', 'completed', 1),
(17, '2025-07-28', 'cancelled', 2),
(19, '2025-07-28', 'paid', 2),
(22, '2025-07-31', 'completed', 1),
(23, '2025-08-01', 'completed', 1),
(28, '2025-08-03', 'completed', 1),
(32, '2025-08-03', 'cancelled', 1),
(37, '2025-08-03', 'paid', 1),
(43, '2025-08-03', 'paid', 1),
(47, '2025-08-03', 'completed', 1),
(49, '2025-08-04', 'completed', 3),
(51, '2025-08-04', 'paid', 1),
(55, '2025-08-04', 'cancelled', 3);

-- --------------------------------------------------------

--
-- Table structure for table `productcategories`
--

CREATE TABLE `productcategories` (
  `categoryId` int(11) NOT NULL,
  `category` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `productcategories`
--

INSERT INTO `productcategories` (`categoryId`, `category`) VALUES
(1, 'Digital Displays'),
(2, 'Integrated Circuits'),
(3, 'LEDs'),
(4, 'Power Supply and Modules'),
(5, 'Wires and Cables'),
(6, 'Breadboards'),
(7, 'Switches'),
(8, 'Resistors'),
(9, 'Capacitors'),
(10, 'Oscilloscopes'),
(11, 'Others');

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `productId` int(11) NOT NULL,
  `productName` varchar(100) NOT NULL,
  `productPrice` decimal(10,0) NOT NULL,
  `productCategories_categoryId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`productId`, `productName`, `productPrice`, `productCategories_categoryId`) VALUES
(1, '1 inch 7 Segment Display Common Anode | SH110', 35, 1),
(2, '4 digit 7 Segment display | SH3461AS', 40, 1),
(3, '8-Pin IC SMD Timer NE555 IC 555 Timer (10 pcs', 49, 2),
(4, 'SN74LS08N, Quad 2-Input AND Logic Gate', 40, 2),
(5, 'SN74LS32N, Quad 2-Input OR Logic Gate', 40, 2),
(6, 'SN74LS04N, Inverter or NOT Logic Gate', 35, 2),
(7, 'LED 5mm (10 pcs)', 25, 3),
(8, 'MCU Micro USB Breadboard 5V Power Supply Module', 35, 4),
(9, '9V Alkaline Battery', 27, 4),
(10, 'Breadboard Power Supply 3.3V or 5V with USB Port', 57, 4),
(11, 'Linear Voltage Regulator 3.3V 800mA LD1117-3.', 25, 4),
(12, '40-Pin Jumper Wires (various sizes/types)', 50, 5),
(13, 'Battery 9V Buckle with 15cm Cable', 15, 5),
(14, 'Breadboard MB102 830 Point', 65, 6),
(15, 'Breadboard MB102 830 Point Clear', 65, 6),
(16, 'Breadboard 400 Tie Point Interlocking', 45, 6),
(17, 'Breadboard 400 Tie Point Interlocking Crystal', 45, 6),
(18, 'Breadboard Mini 4.5x3.5cm Clear', 15, 6),
(19, 'Breadboard SYB-120 700 pins', 55, 6),
(20, 'Breadboard SYB-46 300pins', 35, 6),
(21, 'Tactile Button Switch 6mm (20 pcs)', 25, 7),
(22, 'Resistor 470 ohm 5% 1/4W (10 pcs)', 19, 8),
(23, 'Resistor 4.7k ohm 5% 1/4W (10 pcs)', 19, 8),
(24, 'Resistor 47k ohm 5% 1/4W (10 pcs)', 19, 8),
(25, 'Resistor 2.2k ohm 5% 1/4W (10 pcs)', 19, 8),
(26, 'Resistor 22k ohm 5% 1/4W (10 pcs)', 19, 8),
(27, 'Resistor 100k ohm 5% 1/4W (10 pcs)', 19, 8),
(28, 'Electrolytic Capacitor 12V 1uF-470uF (120 pcs', 95, 9),
(29, 'Electrolytic Capacitor 16V 100uF (10 pcs)', 19, 9),
(30, 'Electrolytic Capacitor 16V 220uF (10 pcs)', 25, 9),
(31, 'Electrolytic Capacitor 25V 2200uF (10 pcs)', 85, 9),
(32, 'Electrolytic Capacitor 400V 100uF (10 pcs)', 55, 9),
(33, 'Electrolytic Capacitor 100V 470uF (10 pcs)', 79, 9),
(34, 'Electrolytic Capacitor 10V 4700uF (10 pcs)', 99, 9),
(35, 'Oscilloscope Handheld DMM', 16750, 10),
(36, 'Oscilloscope with 2-Channel Handheld DMM', 13750, 10),
(37, 'Battery Holder 18650 Dual Slot', 39, 5);

-- --------------------------------------------------------

--
-- Table structure for table `sales`
--

CREATE TABLE `sales` (
  `salesId` int(11) NOT NULL,
  `salesDate` date NOT NULL,
  `orderId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `sales`
--

INSERT INTO `sales` (`salesId`, `salesDate`, `orderId`) VALUES
(2, '2025-08-01', 10),
(3, '2025-08-01', 11),
(4, '2025-08-02', 3),
(5, '2025-08-03', 23),
(6, '2025-08-03', 22),
(7, '2025-08-03', 28),
(8, '2025-08-03', 13),
(9, '2025-08-04', 49),
(10, '2025-08-04', 47);

-- --------------------------------------------------------

--
-- Table structure for table `suppliers`
--

CREATE TABLE `suppliers` (
  `supplierId` int(11) NOT NULL,
  `supplierName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `suppliers`
--

INSERT INTO `suppliers` (`supplierId`, `supplierName`) VALUES
(1, 'Makerlab Electronics'),
(2, 'Circuitrocks'),
(3, 'element14'),
(4, 'RS Components');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `userId` int(11) NOT NULL,
  `username` varchar(45) NOT NULL,
  `password` char(64) DEFAULT NULL,
  `userRole` enum('admin','inventory_manager','order_manager','financial_auditor','customer') DEFAULT 'customer'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`userId`, `username`, `password`, `userRole`) VALUES
(1, 'ronjeremy', 'e52827c72dfac6a188bafd9d09d9295d47f2bd9cd59b378f86c2be4f97ac1860', 'customer'),
(2, 'lilith', '6df2c800dd6a5906d4b06ca9fa788fb089e1a8f98cec27c7569eb047d477f3d9', 'admin'),
(3, 'jeremy', 'a62ee5ab3e8914010c0f75ff149f9415c839c64ccf4d8ed91d13b456dbc1d813', 'admin'),
(7, 'ironman', 'dda820158d142a0b73380cd3efa9ff7d9fa11336e9ba4b3693098964537d61a3', 'admin'),
(8, 'twilight', 'aefe4867ea407215f6c85fc459306f2451950efbe8ea2e91fe5521c249368b88', 'admin'),
(9, 'donnxd', 'eda7a59466adce3a5349eaac69812f703155f2c8f37b10146a334281977f61a0', 'customer'),
(10, 'pinkie', '558211ed72b2d6967037419dff6f1e7cfd002d178c8fdeeb1239760d4e4c4059', 'admin');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin_users`
--
ALTER TABLE `admin_users`
  ADD PRIMARY KEY (`adminId`),
  ADD KEY `userId` (`userId`);

--
-- Indexes for table `cart`
--
ALTER TABLE `cart`
  ADD PRIMARY KEY (`cartId`,`products_productId`,`customers_customerId`),
  ADD KEY `fk_cart_products1_idx` (`products_productId`),
  ADD KEY `fk_cart_customers1_idx` (`customers_customerId`);

--
-- Indexes for table `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`customerId`),
  ADD KEY `fk_customers_userId` (`userId`);

--
-- Indexes for table `expenses`
--
ALTER TABLE `expenses`
  ADD PRIMARY KEY (`expensesId`);

--
-- Indexes for table `inventory`
--
ALTER TABLE `inventory`
  ADD PRIMARY KEY (`inventoryId`,`products_productId`,`suppliers_supplierId`),
  ADD KEY `fk_inventory_products_idx` (`products_productId`),
  ADD KEY `fk_inventory_suppliers1_idx` (`suppliers_supplierId`);

--
-- Indexes for table `orderitems`
--
ALTER TABLE `orderitems`
  ADD PRIMARY KEY (`orderItemsId`),
  ADD KEY `fk_orderItems_orders1_idx` (`orders_orderId`),
  ADD KEY `products_productId` (`products_productId`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`orderId`),
  ADD KEY `customers_customerId` (`customers_customerId`);

--
-- Indexes for table `productcategories`
--
ALTER TABLE `productcategories`
  ADD PRIMARY KEY (`categoryId`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`productId`),
  ADD KEY `fk_products_productCategories1_idx` (`productCategories_categoryId`);

--
-- Indexes for table `sales`
--
ALTER TABLE `sales`
  ADD PRIMARY KEY (`salesId`),
  ADD UNIQUE KEY `orderId` (`orderId`);

--
-- Indexes for table `suppliers`
--
ALTER TABLE `suppliers`
  ADD PRIMARY KEY (`supplierId`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`userId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admin_users`
--
ALTER TABLE `admin_users`
  MODIFY `adminId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `cart`
--
ALTER TABLE `cart`
  MODIFY `cartId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=59;

--
-- AUTO_INCREMENT for table `customers`
--
ALTER TABLE `customers`
  MODIFY `customerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `expenses`
--
ALTER TABLE `expenses`
  MODIFY `expensesId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `inventory`
--
ALTER TABLE `inventory`
  MODIFY `inventoryId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=50;

--
-- AUTO_INCREMENT for table `orderitems`
--
ALTER TABLE `orderitems`
  MODIFY `orderItemsId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `orderId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=56;

--
-- AUTO_INCREMENT for table `productcategories`
--
ALTER TABLE `productcategories`
  MODIFY `categoryId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `productId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=50;

--
-- AUTO_INCREMENT for table `sales`
--
ALTER TABLE `sales`
  MODIFY `salesId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `suppliers`
--
ALTER TABLE `suppliers`
  MODIFY `supplierId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `userId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `admin_users`
--
ALTER TABLE `admin_users`
  ADD CONSTRAINT `admin_users_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `users` (`userId`) ON DELETE CASCADE;

--
-- Constraints for table `cart`
--
ALTER TABLE `cart`
  ADD CONSTRAINT `fk_cart_customers1` FOREIGN KEY (`customers_customerId`) REFERENCES `customers` (`customerId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_cart_products1` FOREIGN KEY (`products_productId`) REFERENCES `products` (`productId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `customers`
--
ALTER TABLE `customers`
  ADD CONSTRAINT `fk_customers_userId` FOREIGN KEY (`userId`) REFERENCES `users` (`userId`);

--
-- Constraints for table `inventory`
--
ALTER TABLE `inventory`
  ADD CONSTRAINT `fk_inventory_products` FOREIGN KEY (`products_productId`) REFERENCES `products` (`productId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_inventory_suppliers1` FOREIGN KEY (`suppliers_supplierId`) REFERENCES `suppliers` (`supplierId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `orderitems`
--
ALTER TABLE `orderitems`
  ADD CONSTRAINT `fk_orderItems_orders1` FOREIGN KEY (`orders_orderId`) REFERENCES `orders` (`orderId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `orderitems_ibfk_1` FOREIGN KEY (`products_productId`) REFERENCES `products` (`productId`);

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`customers_customerId`) REFERENCES `customers` (`customerId`);

--
-- Constraints for table `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `fk_products_productCategories` FOREIGN KEY (`productCategories_categoryId`) REFERENCES `productcategories` (`categoryId`),
  ADD CONSTRAINT `fk_products_productCategories1` FOREIGN KEY (`productCategories_categoryId`) REFERENCES `productcategories` (`categoryId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `sales`
--
ALTER TABLE `sales`
  ADD CONSTRAINT `fk_sales_order` FOREIGN KEY (`orderId`) REFERENCES `orders` (`orderId`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
