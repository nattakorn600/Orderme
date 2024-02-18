-- phpMyAdmin SQL Dump
-- version 4.9.7
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Mar 18, 2022 at 03:41 PM
-- Server version: 5.6.48
-- PHP Version: 7.4.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mahamhor_oderme`
--

-- --------------------------------------------------------

--
-- Table structure for table `menu`
--

CREATE TABLE `menu` (
  `id` int(11) NOT NULL,
  `name` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `price` int(5) NOT NULL,
  `image` text COLLATE utf8_unicode_ci NOT NULL,
  `type` text COLLATE utf8_unicode_ci NOT NULL,
  `other` text COLLATE utf8_unicode_ci NOT NULL,
  `otherprice` text COLLATE utf8_unicode_ci NOT NULL,
  `shop_id` int(11) DEFAULT NULL,
  `status` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `menu`
--

INSERT INTO `menu` (`id`, `name`, `price`, `image`, `type`, `other`, `otherprice`, `shop_id`, `status`) VALUES
(1, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ต้ม', 'หมู,ไก่', '10,5', 16884, 1),
(2, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'กับข้าว', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16884, 1),
(3, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ผัด', 'หมู,ไก่', '10,5', 16884, 1),
(4, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'ยำ', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16884, 1),
(5, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ทอด', 'หมู,ไก่', '10,5', 16884, 1),
(6, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'อาหารจานเดียว,ยำ', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16884, 1),
(7, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ต้ม,กับข้าว', 'หมู,ไก่', '10,5', 16884, 1),
(8, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'อาหารจานเดียว,ยำ', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16884, 1),
(9, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'ทอด', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16884, 1),
(10, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'ผัด', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16884, 1),
(11, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ต้ม', 'หมู,ไก่', '10,5', 12, 1),
(12, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'กับข้าว', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 12, 1),
(13, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'อาหารจานเดียว', 'หมู,ไก่', '10,5', 12, 1),
(14, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'ยำ', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 12, 1),
(15, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ต้ม,กับข้าว', 'หมู,ไก่', '10,5', 12, 1),
(16, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'อาหารจานเดียว,ยำ', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 12, 1),
(17, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ต้ม,กับข้าว', 'หมู,ไก่', '10,5', 12, 1),
(18, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'อาหารจานเดียว,ยำ', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 12, 1),
(19, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'ทอด', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 12, 1),
(20, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'ผัด', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 12, 1),
(21, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ต้ม', 'หมู,ไก่', '10,5', 16888, 1),
(22, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'กับข้าว', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16888, 1),
(23, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'อาหารจานเดียว', 'หมู,ไก่', '10,5', 16888, 1),
(24, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'ยำ', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16888, 1),
(25, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ต้ม,กับข้าว', 'หมู,ไก่', '10,5', 16888, 1),
(26, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'อาหารจานเดียว,ยำ', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16888, 1),
(27, 'JOVINA', 35, 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', 'ต้ม,กับข้าว', 'หมู,ไก่', '10,5', 16888, 1),
(28, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'อาหารจานเดียว,ยำ', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16888, 1),
(29, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'ทอด', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16888, 1),
(30, 'LovePotion', 50, 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', 'ผัด', 'ไส้กรอก,หมูยอ,แซลมอน', '5,7,15', 16888, 1),
(34, 'ป๊อปคอร์น', 25, 'https://mahamhorr.com/odermeApp/image/menu/20211207233214.jpg', 'หวาน', 'รสสาหร่าย,รสโอวัลติน,รสปาปีก้า,รสวิ้งแซ่บ,รสชีส,รสวาซาบิ', '5,5,5,5,5,5', NULL, 1),
(36, 'สเต็ก​โคขุน', 119, 'https://mahamhorr.com/odermeApp/image/menu/20211214103719.jpg', 'คาว', 'เฟรนฟราย,สัปะรด​,สลัด', '10,5,15', 16917, 1),
(48, 'เค้ก', 50, 'https://mahamhorr.com/odermeApp/image/menu/20220113130743.JPG', 'หวาน', 'ท็อปปิ้ง', '10', 16917, 1),
(49, 'ข้าวแกงกะหรี่​', 50, 'https://mahamhorr.com/odermeApp/image/menu/20220206184513.jpg', 'คาว', 'หมู,ไก่,ข้าว', '10,10,5', 16915, 1),
(50, 'ข้าวผัดไข่', 30, 'https://mahamhorr.com/odermeApp/image/menu/20220212202253.jpg', 'ผัด', 'ข้าว,หมู,ไก่,ไข่', '5,15,10,5', 16915, 1),
(51, 'สเต๊กเนื้อ (AUS)​', 189, 'https://mahamhorr.com/odermeApp/image/menu/20220214123121.jpg', 'คาว', 'มันบด,เห็ด', '15,10', NULL, 1),
(52, '7', 8, 'https://mahamhorr.com/odermeApp/image/menu/20220214124358.jpg', 'คาว', 'ก', '8', NULL, 1),
(53, 'สเต๊กเนื้อ(AUS)​', 189, 'https://mahamhorr.com/odermeApp/image/menu/20220214124556.jpg', 'คาว', 'มันบด,เห็ด', '15,10', NULL, 1),
(54, 'ข้าวโพดคั่ว', 30, 'https://mahamhorr.com/odermeApp/image/menu/20220301221319.jpg', 'หวาน', 'รสสาหร่าย,รสชีส', '5,5', 16915, 1),
(55, 'ข้าวโพดคั่ว', 30, 'https://mahamhorr.com/odermeApp/image/menu/20220301221625.jpg', 'หวาน', 'รสสาหร่าย', '5', NULL, 1),
(56, 'กดเเ', 68, 'https://mahamhorr.com/odermeApp/image/menu/20220305165113.jpg', 'ดาว', '', '', NULL, 1),
(57, 'ดาส', 50, 'https://mahamhorr.com/odermeApp/image/menu/20220305165249.jpg', 'พาน', '', '', 16915, 1);

-- --------------------------------------------------------

--
-- Table structure for table `menu_order`
--

CREATE TABLE `menu_order` (
  `id` int(11) NOT NULL,
  `order_id` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `menu_id` int(11) NOT NULL,
  `other` text COLLATE utf8_unicode_ci,
  `other_price` text COLLATE utf8_unicode_ci,
  `number` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `menu_order`
--

INSERT INTO `menu_order` (`id`, `order_id`, `menu_id`, `other`, `other_price`, `number`) VALUES
(21, '20211203192622810', 2, 'แซลมอน,ไส้กรอก', '15,5', 2),
(22, '20211203194456261', 2, 'ไส้กรอก,หมูยอ', '5,7', 1),
(23, '20211203231438278', 7, 'ไก่', '5', 1),
(24, '20211203231438278', 10, '', '', 1),
(25, '20211203231438278', 6, 'หมูยอ,ไส้กรอก', '7,5', 1),
(26, '20211203231438278', 6, 'แซลมอน,หมูยอ', '15,7', 2),
(27, '20211204124822999', 25, 'ไก่', '5', 1),
(28, '20211204124935149', 25, 'ไก่', '5', 1),
(29, '20211204124935149', 27, 'ไก่,หมู', '5,10', 1),
(30, '20211204225836885', 9, 'ไส้กรอก,แซลมอน', '5,15', 1),
(31, '20211204225836885', 5, 'ไก่', '5', 2),
(32, '20211204225902380', 9, 'ไส้กรอก,แซลมอน', '5,15', 1),
(33, '20211204225902380', 5, 'ไก่', '5', 2),
(34, '20211204225902380', 2, 'หมูยอ', '7', 3),
(35, '20211211001610877', 35, 'ข้าวสวย,ข้าวมัน', '0,5', 1),
(36, '20211211001610877', 35, 'น้ำซุป,พิเศษ', '0,10', 3),
(37, '20211211001610877', 35, '', '', 2),
(38, '20211214233049863', 35, 'ข้าวสวย,ข้าวมัน', '0,5', 1),
(39, '20211214233049863', 34, 'รสปาปีก้า,รสโอวัลติน', '5,5', 2),
(40, '20211216114437114', 36, 'เฟรนฟราย,สลัด', '10,15', 1),
(41, '20211216190629926', 21, 'ไก่', '5', 1),
(42, '20211216190629926', 25, 'หมู', '10', 1),
(43, '20211216190629926', 22, 'หมูยอ', '7', 1),
(44, '20211217103132769', 36, 'สลัด', '15', 1),
(45, '20211217103132769', 36, 'สัปะรด​,เฟรนฟราย', '5,10', 1),
(46, '20211217103132769', 36, 'สัปะรด​', '5', 1),
(47, '20211217104305409', 36, 'สัปะรด​', '5', 1),
(48, '20211217104305409', 36, 'เฟรนฟราย', '10', 1),
(49, '20211217104305409', 36, '', '', 1),
(50, '20211217104305409', 36, 'สลัด', '15', 1),
(51, '20211217104305409', 36, 'เฟรนฟราย,สัปะรด​,สลัด', '10,5,15', 1),
(52, '20211221121935117', 36, 'เฟรนฟราย', '10', 1),
(53, '20211221121935117', 36, 'สัปะรด​', '5', 1),
(54, '20211221121935117', 36, 'สลัด', '15', 1),
(55, '20211221121935117', 36, 'เฟรนฟราย,สัปะรด​', '10,5', 1),
(56, '20211221121935117', 36, 'สลัด,สัปะรด​', '15,5', 1),
(57, '20211221130012721', 36, 'เฟรนฟราย', '10', 1),
(58, '20211221130012721', 36, 'สลัด', '15', 1),
(59, '20211221130311336', 36, 'เฟรนฟราย', '10', 2),
(60, '20211222135831855', 36, 'เฟรนฟราย', '10', 1),
(61, '20220113092610258', 36, 'สลัด,เฟรนฟราย', '15,10', 2),
(62, '20220113094323125', 36, 'สลัด', '15', 2),
(63, '20220113104142918', 36, 'เฟรนฟราย,สลัด', '10,15', 2),
(64, '20220113130256165', 36, 'เฟรนฟราย,สลัด', '10,15', 3),
(65, '20220206195033662', 34, 'รสสาหร่าย,รสปาปีก้า', '5,5', 1),
(66, '20220206195033662', 49, 'ไก่,ข้าว', '10,5', 1),
(67, '20220206195033662', 34, 'รสชีส', '5', 2),
(68, '20220206200623453', 34, 'รสวิ้งแซ่บ', '5', 1),
(69, '20220206200704386', 49, 'ไก่', '10', 1),
(70, '20220206200740268', 34, 'รสปาปีก้า', '5', 1),
(71, '20220206200740268', 49, 'หมู', '10', 1),
(72, '20220206195033661', 34, 'รสสาหร่าย,รสปาปีก้า', '5,5', 1),
(73, '20220206195033661', 49, 'ไก่,ข้าว', '10,5', 1),
(74, '20220206195033661', 34, 'รสชีส', '5', 2),
(75, '20220206200623452', 34, 'รสวิ้งแซ่บ', '5', 1),
(76, '20220206200704385', 49, 'ไก่', '10', 1),
(77, '20220206200740267', 34, 'รสปาปีก้า', '5', 1),
(78, '20220206200740267', 49, 'หมู', '10', 1),
(79, '20220210194136940', 49, 'หมู,ข้าว', '10,5', 1),
(80, '20220301160414906', 49, 'ข้าว', '5', 2),
(81, '20220301234812666', 50, 'ข้าว', '5', 1),
(82, '20220301234812666', 54, 'รสสาหร่าย,รสชีส', '5,5', 1),
(83, '20220301234812666', 49, 'ไก่,หมู,ข้าว', '10,10,5', 1),
(84, '20220301234812667', 49, 'ไก่,หมู,ข้าว', '10,10,5', 1),
(85, '20220301234812667', 50, 'ข้าว', '5', 1),
(86, '20220301234812667', 54, 'รสสาหร่าย,รสชีส', '5,5', 1);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `id` int(11) NOT NULL,
  `order_id` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `user_id` int(11) NOT NULL,
  `shop_id` int(11) DEFAULT NULL,
  `price` int(11) NOT NULL,
  `payment` text COLLATE utf8_unicode_ci,
  `tables` int(11) NOT NULL,
  `status` int(2) NOT NULL,
  `date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`id`, `order_id`, `user_id`, `shop_id`, `price`, `payment`, `tables`, `status`, `date`) VALUES
(20, '20211204124822999', 7, 16888, 40, 'https://mahamhorr.com/odermeApp/image/bill/20211204124822999.jpg', 0, 1, '2021-12-04 05:48:22'),
(21, '20211204124935149', 7, 16884, 90, 'https://mahamhorr.com/odermeApp/image/bill/20211204124935149.jpg', 0, 2, '2021-12-04 05:49:35'),
(26, '20211216114437114', 7, 16917, 144, 'https://mahamhorr.com/odermeApp/image/bill/20211216114437114.jpeg', 3, 5, '2021-12-16 04:44:37'),
(37, '20211216190629926', 7, 16888, 142, 'https://mahamhorr.com/odermeApp/image/bill/20211216190629926.jpg', 3, 1, '2021-12-16 12:06:29'),
(38, '20211217103132769', 7, 16917, 392, 'https://mahamhorr.com/odermeApp/image/bill/20211217103132769.jpg', 3, 5, '2021-12-17 03:31:32'),
(39, '20211217104305409', 7, 16917, 655, 'https://mahamhorr.com/odermeApp/image/bill/20211217104305409.jpg', 3, 5, '2021-12-17 03:43:05'),
(40, '20211221121935117', 7, 16917, 660, 'https://mahamhorr.com/odermeApp/image/bill/20211221123516323.jpg', 77, 5, '2021-12-21 05:19:35'),
(41, '20211221130012721', 7, 16917, 263, 'https://mahamhorr.com/odermeApp/image/bill/20211221130012721.jpg', 77, 4, '2021-12-21 06:00:12'),
(42, '20211221130311336', 7, 16917, 258, 'https://mahamhorr.com/odermeApp/image/bill/20211221130311336.jpg', 77, 4, '2021-12-21 06:03:11'),
(43, '20211222135831855', 7, 16917, 129, 'https://mahamhorr.com/odermeApp/image/bill/20211222135831855.jpg', 77, 4, '2021-12-22 06:58:31'),
(44, '20220113092610258', 7, 16917, 288, 'https://mahamhorr.com/odermeApp/image/bill/20220113092910638.PNG', 2, 5, '2022-01-13 02:26:10'),
(45, '20220113094323125', 7, 16917, 268, 'https://mahamhorr.com/odermeApp/image/bill/20220113094323125.PNG', 2, 4, '2022-01-13 02:43:23'),
(46, '20220113104142918', 7, 16917, 288, 'https://mahamhorr.com/odermeApp/image/bill/20220113104142918.jpeg', 2, 4, '2022-01-13 03:41:42'),
(47, '20220113130256165', 7, 16917, 432, 'https://mahamhorr.com/odermeApp/image/bill/20220113130256165.jpeg', 2, 4, '2022-01-13 06:02:56'),
(48, '20220206195033662', 5, 16915, 160, 'https://mahamhorr.com/odermeApp/image/bill/20220206195033662.PNG', 3, 5, '2022-02-06 12:50:33'),
(49, '20220206200623453', 5, 16915, 30, 'https://mahamhorr.com/odermeApp/image/bill/20220206200623453.jpg', 3, 5, '2022-02-06 13:06:23'),
(50, '20220206200704386', 5, 16915, 60, 'https://mahamhorr.com/odermeApp/image/bill/20220206200704386.PNG', 3, 5, '2022-02-06 13:07:04'),
(51, '20220206200740268', 5, 16915, 90, 'https://mahamhorr.com/odermeApp/image/bill/20220206200740268.PNG', 3, 5, '2022-02-05 13:07:40'),
(52, '20220206195033661', 5, 16917, 160, 'https://mahamhorr.com/odermeApp/image/bill/20220206195033662.PNG', 3, 5, '2022-02-04 12:50:33'),
(53, '20220206200623452', 7, 16915, 30, 'https://mahamhorr.com/odermeApp/image/bill/20220206200623453.jpg', 3, 5, '2022-02-04 13:06:02'),
(54, '20220206200704385', 7, 1691, 60, 'https://mahamhorr.com/odermeApp/image/bill/20220206200704386.PNG', 3, 5, '2022-02-05 13:07:04'),
(55, '20220206200740267', 7, 16915, 90, 'https://mahamhorr.com/odermeApp/image/bill/20220206200740268.PNG', 3, 4, '2022-02-05 13:07:40'),
(56, '20220210194136940', 5, 16915, 65, 'https://mahamhorr.com/odermeApp/image/bill/20220210194136940.PNG', 3, 4, '2022-02-10 12:41:36'),
(57, '20220301160414906', 7, 16915, 110, 'https://mahamhorr.com/odermeApp/image/bill/20220301160414906.PNG', 3, 3, '2022-03-01 09:04:14'),
(58, '20220301234812666', 7, 16915, 150, 'https://mahamhorr.com/odermeApp/image/bill/20220301234812666.PNG', 3, 2, '2022-03-01 16:48:12'),
(59, '20220301234812667', 7, 16915, 150, 'https://mahamhorr.com/odermeApp/image/bill/20220301234812666.PNG', 3, 2, '2022-03-01 16:48:12');

-- --------------------------------------------------------

--
-- Table structure for table `review`
--

CREATE TABLE `review` (
  `id` int(11) NOT NULL,
  `point` int(11) NOT NULL,
  `comment` text COLLATE utf8_unicode_ci NOT NULL,
  `order_id` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `shop_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `review`
--

INSERT INTO `review` (`id`, `point`, `comment`, `order_id`, `shop_id`, `user_id`) VALUES
(1, 4, 'ดี', '4', 16884, 5),
(2, 4, 'ดี', '4', 16896, 5),
(8, 5, 'ดีมาก', '19', 16884, 5),
(9, 4, 'ได้', '17', 16884, 5),
(10, 3, 'อร่อย', '38', 16917, 7),
(11, 3, '2', '39', 16917, 7),
(12, 4, 'ดี', '40', 16917, 7),
(13, 4, 'อร่อย', '44', 16917, 7),
(14, 4, 'อร่อย', '26', 16917, 7),
(15, 4, 'อร่อย', '48', 16916, 5),
(16, 4, 'อร่อยนะนะ', '57', 16916, 5);

-- --------------------------------------------------------

--
-- Table structure for table `shop`
--

CREATE TABLE `shop` (
  `id` int(11) NOT NULL,
  `name` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `address` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `image` text COLLATE utf8_unicode_ci,
  `background` text COLLATE utf8_unicode_ci,
  `latitude` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `longitude` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `prompt_number` varchar(13) COLLATE utf8_unicode_ci NOT NULL,
  `prompt_name` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `user_id` int(11) NOT NULL,
  `status` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `shop`
--

INSERT INTO `shop` (`id`, `name`, `address`, `image`, `background`, `latitude`, `longitude`, `prompt_number`, `prompt_name`, `user_id`, `status`) VALUES
(2, 'SherPeach​', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/2/20200824224344.jpg', '', '16.7540732459754', '100.190634056926', '1669800248610', 'ณัฐกร ขุนอาจ', 0, 1),
(3, 'แม่ค้าปากหมา', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/3/20200825001149.jpg', '', '16.7488831787712', '100.195924043655', '', '', 0, 1),
(4, 'มานี่ บราวนี่', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/4/20200825001617.jpg', '', '16.7489117524594', '100.196510776877', '', '', 0, 1),
(5, 'JOVINA', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', '', '16.748', '100.189', '1669800248610', 'ณัฐกร ขุนอาจ', 0, 1),
(6, 'B Crizpy', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/6/20200825002550.jpg', '', '16.748766', '100.189678', '1669800248610', 'ณัฐกร ขุนอาจ', 0, 1),
(7, 'LovePotion', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', '', '16.7556136085995', '100.191200338304', '', '', 0, 1),
(8, 'Happy Blink', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/8/20200825004939.jpg', '', '16.7537595857808', '100.19089255482', '', '', 0, 1),
(9, 'August', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/9/20200825005232.jpg', '', '16.7542835296445', '100.191375687718', '', '', 0, 1),
(10, 'แดรี่โด', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/10/20200825005515.jpg', '', '13.823688979156692', '100.58396291566152', '1669800248610', 'ณัฐกร ขุนอาจ', 0, 1),
(11, 'For Fang', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/11/20200825005821.jpg', '', '16.7545435747766', '100.189450867474', '', '', 0, 1),
(12, 'HoySri', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/12/20200825010048.jpg', '', '16.755389842648', '100.188995562494', '', '', 0, 1),
(13, 'TINY', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/13/20200825010321.jpg', '', '16.7496276985277', '100.197019055486', '', '', 0, 1),
(16876, 'JOVINA', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', '', '16.125', '100.74', '', '', 0, 1),
(16877, 'B Crizpy', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/6/20200825002550.jpg', '', '16.12', '100.733', '', '', 0, 1),
(16878, 'SherPeach​', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/2/20200824224344.jpg', '', '16.13', '100.75', '', '', 0, 1),
(16879, 'แม่ค้าปากหมา', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/3/20200825001149.jpg', '', '16.119', '100.735', '', '', 0, 1),
(16880, 'มานี่ บราวนี่', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/4/20200825001617.jpg', '', '16.12', '100.727', '', '', 0, 1),
(16881, 'LovePotion', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', '', '16.118', '100.7275', '', '', 0, 1),
(16882, 'Happy Blink', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/8/20200825004939.jpg', '', '16.117', '100.732', '', '', 0, 1),
(16883, 'August', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/9/20200825005232.jpg', '', '16.114', '100.728', '', '', 0, 1),
(16884, 'แดรี่โด', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/10/20200825005515.jpg', '', '16.1216603', '100.722781', '1669800248610', 'ณัฐกร ขุนอาจ', 0, 1),
(16885, 'For Fang', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/11/20200825005821.jpg', '', '16.1107082', '100.73', '', '', 0, 1),
(16886, 'HoySri', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/12/20200825010048.jpg', '', '16.12', '100.722', '', '', 0, 1),
(16887, 'TINY', 'เลขที่ 99 หมู่ 9 ตำบล ดงขุย อำเภอ ชนแดน จังหวัด เพชรบูรณ์ 67190', 'https://mahamhorr.com/testmobile/User/13/20200825010321.jpg', '', '16.1216604', '100.7227894', '', '', 0, 1),
(16888, 'JOVINA', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/5/20200825001951.jpg', '', '13.724147273391177', '100.50228927914017', '0953827434', 'ญาณวรุตม์  คงยนต์', 0, 1),
(16889, 'B Crizpy', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/6/20200825002550.jpg', '', '16.221887', '100.41702', '', '', 0, 1),
(16890, 'SherPeach​', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/2/20200824224344.jpg', '', '16.22349', '100.41711', '', '', 0, 1),
(16891, 'แม่ค้าปากหมา', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/3/20200825001149.jpg', '', '16.22361', '100.41730', '', '', 0, 1),
(16892, 'มานี่ บราวนี่', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/4/20200825001617.jpg', '', '16.22454', '100.41874', '', '', 0, 1),
(16893, 'LovePotion', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/7/20200825003505.jpg', '', '16.22459', '100.41457', '', '', 0, 1),
(16894, 'Happy Blink', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/8/20200825004939.jpg', '', '16.22391', '100.41432', '', '', 0, 1),
(16895, 'August', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/9/20200825005232.jpg', '', '16.2243', '100.41911', '', '', 0, 1),
(16896, 'แดรี่โด', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/10/20200825005515.jpg', '', '16.21874', '100.42988', '1669800248610', 'ณัฐกร ขุนอาจ', 0, 1),
(16897, 'For Fang', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/11/20200825005821.jpg', '', '16.21806', '100.43471', '', '', 0, 1),
(16898, 'HoySri', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/12/20200825010048.jpg', '', '16.21768', '100.43565', '', '', 0, 1),
(16899, 'TINY', 'เลขที่ 99 หมู่ 9 ตำบล ท่าโพธิ์ ตำบล ท่าโพธิ์ อำเภอ เมืองพิษณุโลก พิษณุโลก 65000', 'https://mahamhorr.com/testmobile/User/13/20200825010321.jpg', '', '16.21616', '100.43567', '', '', 0, 1),
(16915, 'ร้านอาหาร1', 'ดงขุย ชนแดน เพชรบูรณ์', 'https://mahamhorr.com/odermeApp/image/shop/20220301212447.jpg', 'https://mahamhorr.com/odermeApp/image/shop/20220301212529.jpg', '16.119400', '100.723004', '0962756567', 'ณัฐกร ขุนอาจ', 5, 1),
(16917, 'Swift', '11', 'https://mahamhorr.com/odermeApp/image/shop/20211118125442.jpeg', 'https://mahamhorr.com/odermeApp/image/shop/20211118125443.jpeg', '13.7232527799844', '100.502034694056', '1669800245939', 'ญาณวรุตม์  คงยนต์', 7, 1),
(16921, 'ร้านอาหาร1', 'ดงขุย ชนแดน เพชรบูรณ์', 'https://mahamhorr.com/odermeApp/image/shop/20220301212447.jpg', 'https://mahamhorr.com/odermeApp/image/shop/20220301212529.jpg', '16.11982692', '100.72171832', '1669800248610', 'ณัฐกร ขุนอาจ', 5, 0);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `social_id` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `name` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `picture` text COLLATE utf8_unicode_ci NOT NULL,
  `phone` int(10) DEFAULT NULL,
  `shop_id` int(11) DEFAULT NULL,
  `type` varchar(10) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `social_id`, `email`, `name`, `picture`, `phone`, `shop_id`, `type`) VALUES
(5, '115499902979669292440', 'nattakornk60@nu.ac.th', '60361217 ณัฐกร ขุนอาจ', 'https://lh3.googleusercontent.com/a-/AOh14GhTI_HrO4Zgj10pzkeeRX58HstFHs1Pzest2G16', NULL, 16915, 'Google'),
(6, '4264943593591346', 'nattakorn_boat@hotmail.com', '60361217 ณัฐกร ขุนอาจ', 'https://lh3.googleusercontent.com/a-/AOh14GhTI_HrO4Zgj10pzkeeRX58HstFHs1Pzest2G16', NULL, NULL, 'Facebook'),
(7, '114817820823763192454', 'yanawarutk60@nu.ac.th', '๖๐๓๖๑๐๙๕ ญาณวรุตม์ คงยนต์', 'https://autisticdating.net/imgs/profile-placeholder.jpg', NULL, 16917, 'Google'),
(8, '103769828042520435856', 'yanawarut636@gmail.com', 'yanawarut khongyon', 'https://autisticdating.net/imgs/profile-placeholder.jpg', NULL, NULL, 'Google'),
(11, '4645090362243332', 'nattakorn_boat@hotmail.com', 'Boat Nattakorn', 'https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=4645090362243332&height=50&width=50&ext=1646635313&hash=AeR8jTUePjORj9r6DT0', NULL, NULL, 'Facebook'),
(12, '4121689497933643', 'yanawarut636@gmail.com', 'Yanawarut Khongyon', 'https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=4121689497933643&height=50&width=50&ext=1648753005&hash=AeRyG83Yle9H1IrewDU', NULL, NULL, 'Facebook'),
(13, '115769691747070967073', 'yanawarutk@gmail.com', '60361217 ณัฐกร ขุนอาจ', 'https://lh3.googleusercontent.com/a-/AOh14GhTI_HrO4Zgj10pzkeeRX58HstFHs1Pzest2G16', NULL, NULL, 'Google');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `menu`
--
ALTER TABLE `menu`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `menu_order`
--
ALTER TABLE `menu_order`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `review`
--
ALTER TABLE `review`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `shop`
--
ALTER TABLE `shop`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `menu`
--
ALTER TABLE `menu`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=58;

--
-- AUTO_INCREMENT for table `menu_order`
--
ALTER TABLE `menu_order`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=87;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=60;

--
-- AUTO_INCREMENT for table `review`
--
ALTER TABLE `review`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `shop`
--
ALTER TABLE `shop`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16922;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
