// @mui
import { styled } from '@mui/material/styles';
import { Box, Stack, Card, Typography, Container, Grid, Button, Link, CardMedia, CardContent, Rating, CardActions, IconButton } from '@mui/material';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import ArrowForwardOutlinedIcon from '@mui/icons-material/ArrowForwardOutlined';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import { Link as RouterLink } from 'react-router-dom';
import Slider from 'react-slick';
import { m } from 'framer-motion';
import { MotionViewport, varFade } from '../components/animate';
import { PATH_DASHBOARD } from '../routes/paths';
import Image from '../components/Image';
// components
import Page from '../components/Page';
// sections
import {
  HomeHero,
  HomeMinimal,
  HomeDarkMode,
  HomeLookingFor,
  HomeColorPresets,
  HomePricingPlans,
  HomeAdvertisement,
  HomeCleanInterfaces,
  HomeHugePackElements,
} from '../sections/home';
import Navbar from '../layouts/main/Navbar';
import SummerSaleBanner from '../layouts/main/SummerSaleBanner';

// ----------------------------------------------------------------------

const categories = [
  {
    id: 1,
    name: "Fashion Men",
    imageUrl: "/logo/389affe6be1a5141869597013f2162a4.jpg"
  },
  {
    id: 2,
    name: "Fashion Women",
    imageUrl: "/path/to/fashion-women.jpg"
  },
  {
    id: 3,
    name: "Book",
    imageUrl: "/path/to/book.jpg"
  },
  {
    id: 4,
    name: "Snack",
    imageUrl: "/path/to/snack.jpg"
  },
  {
    id: 5,
    name: "Department store",
    imageUrl: "/path/to/department-store.jpg"
  },
  {
    id: 6,
    name: "Beauty & Health",
    imageUrl: "/path/to/beauty-health.jpg"
  },
  {
    id: 7,
    name: "Jewelry",
    imageUrl: "/path/to/jewelry.jpg"
  },
  {
    id: 8,
    name: "Handmade",
    imageUrl: "/path/to/handmade.jpg"
  },
  {
    id: 9,
    name: "Accessory",
    imageUrl: "/path/to/accessory.jpg"
  },
  {
    id: 10,
    name: "Sneakers",
    imageUrl: "/path/to/sneakers.jpg"
  },
  {
    id: 11,
    name: "Gift",
    imageUrl: "/path/to/gift.jpg"
  },
  {
    id: 12,
    name: "Musical instrument",
    imageUrl: "/path/to/musical-instrument.jpg"
  }
];

const products = [
  {
    id: 1,
    name: "C'est Bon Sốt Kem Phô Mai BP",
    price: 2.4,
    originalPrice: null,
    imageUrl: "/logo/389affe6be1a5141869597013f2162a4.jpg",
    rating: 4,
    sale: "50%"
  },
  {
    id: 2,
    name: "Bộng đất sét theo yêu cầu",
    price: 10,
    originalPrice: null,
    imageUrl: "/path/to/play-dough-set.jpg",
    rating: 4
  },
  {
    id: 3,
    name: "Sách đắc nhân tâm",
    price: 5,
    originalPrice: null,
    imageUrl: "/path/to/dac-nhan-tam.jpg",
    rating: 5
  },
  {
    id: 4,
    name: "Sách trẻ em 3 quyển",
    price: 10,
    originalPrice: null,
    imageUrl: "/path/to/children-books.jpg",
    rating: 3
  },
  {
    id: 5,
    name: "Bình giữ nhiệt LocknLock",
    price: 12,
    originalPrice: null,
    imageUrl: "/path/to/locknlock-bottle.jpg",
    rating: 4
  },
  {
    id: 6,
    name: "Ổ điện cho sinh điện",
    price: 5,
    originalPrice: null,
    imageUrl: "/path/to/power-strip.jpg",
    rating: 4
  },
  {
    id: 7,
    name: "iPhone hiệu Samsung",
    price: 1000,
    originalPrice: 2000,
    imageUrl: "/path/to/samsung-phone.jpg",
    rating: 5,
    sale: "50%"
  },
  {
    id: 8,
    name: "Kem chống nắng Anessa",
    price: 10,
    originalPrice: null,
    imageUrl: "/path/to/anessa-sunscreen.jpg",
    rating: 5
  }
];



const RootStyle = styled('div')(() => ({
  height: '100%',
}));

const ContentStyle = styled('div')(({ theme }) => ({
  overflow: 'hidden',
  position: 'relative',
  backgroundColor: theme.palette.background.default,
}));

// ----------------------------------------------------------------------

export default function HomePage() {
  return (
    <Page title="The starting point for your next project">
      <RootStyle>
        <Navbar />
        <ContentStyle>
          <Container component={MotionViewport} sx={{ pb: 10, textAlign: 'center', marginTop: '30px' }}>
            <m.div variants={varFade().inUp}>
              <Box display='flex' justifyContent='space-between' alignContent='center'>
                <Typography variant="h4" sx={{ mb: 3, textAlign: 'start', mt: 5 }}>
                  Popular Categories
                </Typography>
                <Button disableFocusRipple size="small" endIcon={<ArrowForwardOutlinedIcon />}>
                  Shop Now
                </Button>
              </Box>
            </m.div>
            <Box maxWidth="lg" position="relative" m="auto">
              <Grid container spacing={1}>
                {(categories || []).map((category, idx) => (
                  <Grid item xs={3} md={2} key={idx}>
                    <CategoryCard category={category} />
                  </Grid>
                ))}
              </Grid>
            </Box>
            <m.div variants={varFade().inUp}>
              <Box display='flex' justifyContent='space-between' alignContent='center'>
                <Typography variant="h4" sx={{ mb: 3, textAlign: 'start', mt: 5 }}>
                  Popular Products
                </Typography>
                <Button disableFocusRipple size="small" endIcon={<ArrowForwardOutlinedIcon />}>
                  Shop Now
                </Button>
              </Box>
            </m.div>
            <Box maxWidth="lg" position="relative" m="auto">
              <Grid container spacing={1}>
                {(products || []).map((product, idx) => (
                  <Grid item xs={6} md={3} key={idx}>
                    <ProductCard product={product} />
                  </Grid>
                ))}
              </Grid>
            </Box>
          </Container>
          <Container>
            <SummerSaleBanner />
          </Container>
        </ContentStyle>
      </RootStyle>
    </Page>
  );
}
function CategoryCard({ category }) {

  const { name, imageUrl } = category;
  const linkTo = 'PATH_DASHBOARD.shopView(name)';

  return (
    <Card key={name} sx={{
      p: 1,
      borderRadius: 0.5,
      ":hover": {
        boxShadow: 3,
        ".MuiCardMedia-root": {
          opacity: 0.8
        },
        borderColor: 'orange',
        "& .MuiTypography-root": {
          color: 'orange'
        }
      },
      border: 1,
      borderColor: 'transparent',
      overflow: 'hidden'
    }} >
      <Image alt={name} src={imageUrl} ratio="1/1" sx={{ borderRadius: 0.5 }} />
      <Link to={linkTo} color="inherit" component={RouterLink}>
        <Typography variant="subtitle1" noWrap sx={{ mt: 2, mb: 0.5 }}>
          {name}
        </Typography>
      </Link>
    </Card>
  );
}


const StyledRating = styled(Rating)({
  '& .MuiRating-iconFilled': {
    color: '#ff8a00',
  },
  '& .MuiRating-iconHover': {
    color: '#ff8a00',
  },
});


function ProductCard({ product }) {
  const { name, imageUrl, price, originalPrice, rating, sale } = product;

  return (
    <Card sx={{ maxWidth: 345, position: 'relative', m: 1, borderRadius: 0.5 }}>
      {sale && (
        <Typography sx={{
          color: 'white',
          backgroundColor: '#c23908',
          padding: '0.5rem',
          position: 'absolute',
          top: 8,
          left: 8,
          borderRadius: '4px',
          fontSize: '0.75rem'
        }}>
          Sale {sale}
        </Typography>
      )}
      <CardMedia
        component="img"
        height="160"
        image={imageUrl}
        alt={name}
        sx={{ objectFit: 'contain', p: 1 }}
      />
      <CardContent>
        <Box display='flex' justifyContent='space-between'>
          <Stack spacing={0.5}>
            <Typography gutterBottom variant="body2" component="div" noWrap sx={{ fontWeight: 'bold' }}>
              {name}
            </Typography>
            <Box display='flex' justifyContent='space-between' >
              <Typography variant="body2" color="text.secondary" sx={{ display: 'flex', alignItems: 'center' }}>
                <span style={{ color: originalPrice ? '#aaa' : 'inherit', textDecoration: originalPrice ? 'line-through' : 'none' }}>
                  ${originalPrice || price}
                </span>
                {originalPrice && <span style={{ color: 'red', fontWeight: 'bold', marginLeft: 1 }}>${price}</span>}
              </Typography>
              <IconButton aria-label="add to cart" sx={{
                backgroundColor: '#f2f2f2', // Màu nền mà bạn muốn
                color: 'white', // Màu của biểu tượng
                borderRadius: '50%', // Làm tròn nút
                '&:hover': {
                  backgroundColor: '#e64a19', // Màu khi hover
                },
                border: '2px solid #fea63c',
                padding: '8px' // Padding để biểu tượng không chạm vào viền
              }}>
                <ShoppingCartIcon sx={{ color: '#fea63c' }} color='#f2f2f2' />
              </IconButton>
            </Box>
            <StyledRating size='small' name="read-only" value={rating} readOnly />
          </Stack>

        </Box>
      </CardContent>
    </Card>
  );
}
